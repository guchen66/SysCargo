
namespace 仓库管理系统.Parameters
{
    /// <summary>
    /// 注册参数
    /// </summary>
    public class SignUpArgs
    {
        public string SessionId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VerificationCode { get; set; }
    }

    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginArgs
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
