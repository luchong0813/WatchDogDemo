using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogDemo
{
    public static class WatchDog
    {
        private static string processName = "WatchDog";  //看护程序进程名（注意这里不是被看护程序名，你可以试一下换成主程序名字会使什么效果）
        private static string appPath = AppDomain.CurrentDomain.BaseDirectory;	//系统启动目录
        /// <summary>
        /// 启动看门狗
        /// </summary>
        public static void Start()
        {
            try
            {
                string program = string.Format("{0}{1}.exe", appPath, processName);
                ProcessStartInfo info = new ProcessStartInfo
                {
                    WorkingDirectory = appPath,
                    FileName = program,
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    Arguments = Process.GetCurrentProcess().MainModule.FileName  //被看护程序的完整路径
                };
                Process.Start(info);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 停用看门狗
        /// </summary>
        public static void Stop()
        {
            Process[] myproc = Process.GetProcessesByName(processName);
            foreach (Process pro in myproc)
            {
                pro.Kill();
                pro.WaitForExit(3000);
            }
        }
    }
}
