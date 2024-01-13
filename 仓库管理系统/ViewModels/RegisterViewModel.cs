
namespace 仓库管理系统.ViewModels
{


    public class RegisterViewModel : BindableBase
    {
        private readonly LoggerHelper _loggerHelper; // 定义 LoggerHelper 实例
        public RegisterViewModel(LoggerHelper loggerHelper)
        {
            _loggerHelper = loggerHelper;
            DoSomeWork();
        }

        public void DoSomeWork()
        {
            try
            {
                int i=int.MaxValue;
                Console.WriteLine(i+1);
            }
            catch (Exception ex)
            {
                var logMessage = new LogMessage
                {
                    IpAddress = "127.0.0.1",
                    OperationName = "user",
                    OperationTime = DateTime.Now.ToString(),
                    LogInfo = ex.Message, // 记录异常信息
                    StackTrace = ex.StackTrace // 记录堆栈信息
                };
                _loggerHelper.Log(Cargo.Core.Loggers.LogLevel.Error, logMessage); // 记录日志
            }
        }
    }
}
