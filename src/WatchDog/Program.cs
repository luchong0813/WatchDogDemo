using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WatchDog.Properties;

namespace WatchDog
{
    static class Program
    {
        static NotifyIcon icon = new NotifyIcon();
        private static Dog dog = new Dog();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                MessageBox.Show("启动参数异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = args[0];
            if(!File.Exists(filePath))
            {
                MessageBox.Show("启动参数异常", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表 
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程 
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (process.MainModule.FileName.Equals(current.MainModule.FileName))
                    {
                        //已经存在的进程
                        return;
                    }
                    else
                    {
                        process.Kill();
                        process.WaitForExit(3000);
                    }
                }
            }
            icon.Text = "看门狗";
            icon.Visible = true;
            Log.Info("启动看门狗,看护程序:{0}",filePath);
            dog.Start(filePath);
            Application.Run();
        }

    }
}
