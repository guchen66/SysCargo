
namespace 仓库管理系统.Views
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : MetroWindow
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("This is the title", "Some message");
        }
    }
}
