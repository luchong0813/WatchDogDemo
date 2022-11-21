using System.Windows;

namespace WatchDogDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //正常退出程序时请调用停止看门狗的方法，否则依然会自动启动被看护的程序
            WatchDog.Stop();
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            throw new System.Exception("手动抛出一个异常");
        }
    }
}
