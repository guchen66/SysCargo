using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.Parameters
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
