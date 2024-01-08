
namespace 仓库管理系统.Views
{
    /// <summary>
    /// HeaderView.xaml 的交互逻辑
    /// </summary>
    public partial class HeaderView : UserControl
    {

        private readonly DispatcherTimer ShowTimer;  //声明一个计时器
        public HeaderView()
        {
            InitializeComponent();

            //设置计时器
            ShowTimer = new DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurrentTimer);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
        }

       // 实时显示日期时间
        private void ShowCurrentTimer(object sender, EventArgs e)
        {
            string date = DateTime.Now.DayOfWeek.ToString();  //当前日期
            if (date == "Monday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期一"; }
            else if (date == "Tuesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期二"; }
            else if (date == "Wednesday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期三"; }
            else if (date == "Thursday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期四"; }
            else if (date == "Friday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期五"; }
            else if (date == "Saturday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期六"; }
            else if (date == "Sunday") { lblDate.Text = DateTime.Now.ToLongDateString().ToString() + " 星期日"; }
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");  //当前时间
        }

      
    }
}
