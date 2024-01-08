
namespace 仓库管理系统.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public LoginView()
        {
            InitializeComponent();
          //  LoadBackgroundImage();
        }

        private async void LoadBackgroundImage()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("../Assets/Images/Header.jpeg", UriKind.Relative);
            bitmapImage.EndInit();

            await Task.Delay(100); // 添加延迟以确保初始化开始

            // 在 UI 线程上设置图像源
            Dispatcher.Invoke(() =>
            {
                ImageBrush imageBrush = new ImageBrush(bitmapImage);
                this.Background = imageBrush; // 使用 this.Background
            });
        }
    }

}
