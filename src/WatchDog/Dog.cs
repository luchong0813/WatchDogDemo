using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WatchDog
{
    public class Dog
    {
        private Timer timer = new Timer();
        private string processName ;
        private string filePath;//要监控的程序的路径
        public Dog()
        {
            timer.Interval = 5000;
            timer.Tick += timer_Tick;
        }

        public void Start(string filePath)
        {
            this.filePath = filePath;
            this.processName = Path.GetFileNameWithoutExtension(filePath);
            timer.Enabled = true;
        }

        /// <summary>
        /// 定时检测系统是否在运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Process[] myproc = Process.GetProcessesByName(processName);
                if (myproc.Length == 0)
                {
                    Log.Info("检测到看护程序已退出，开始重新激活程序,程序路径:{0}",filePath);
                    ProcessStartInfo info = new ProcessStartInfo
                    {
                        WorkingDirectory = Path.GetDirectoryName(filePath),
                        FileName = filePath,
                        UseShellExecute = true
                    };
                    Process.Start(info);
                    Log.Info("看护程序已启动");
                }
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
