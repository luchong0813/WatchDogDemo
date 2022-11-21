using System;
using System.IO;
using System.Threading;

namespace WatchDog
{
    public class Log
    {
        //读写锁，当资源处于写入模式时，其他线程写入需要等待本次写入结束之后才能继续写入
        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        //日志文件路径
        public static string logPath = "logs\\dog.txt";

        //静态方法todo:在处理话类型之前自动调用，去检查日志文件是否存在
        static Log()
        {
            //创建文件夹
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
        }

        /// <summary>
        /// 写入日志.
        /// </summary>
        public static void Info(string format, params object[] args)
        {
            try
            {
                LogWriteLock.EnterWriteLock();
                string msg = args.Length > 0 ? string.Format(format, args) : format;
                using (FileStream stream = new FileStream(logPath, FileMode.Append))
                {
                    StreamWriter write = new StreamWriter(stream);
                    string content = String.Format("{0} {1}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),msg);
                    write.WriteLine(content);
                    //关闭并销毁流写入文件
                    write.Close();
                    write.Dispose();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
    }
}
