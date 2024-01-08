using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Ui.Helpers
{
    public class MD5Helper
    {
        public string GetMD5Provider2(string source, string salt, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source + salt + sourceFilePath + sourceLineNumber));
                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            return result;
        }
        /// <summary>
        /// 加密操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetMD5Provider(string source, string salt)
        {

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            return result;
        }


        /// <summary>
        /// 解密操作
        /// </summary>
        /// <param name="encryptedPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string DecryptPassword(string encryptedPassword, string salt)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(encryptedPassword));
                string encryptedMd5 = BitConverter.ToString(bytes).Replace("-", "");

                if (encryptedMd5 == encryptedPassword)
                {
                    // 解密密码
                    byte[] originalBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(salt));
                    return Encoding.UTF8.GetString(originalBytes);
                }
                else
                {
                    // 密码已经是明文了
                    return encryptedPassword;
                }
            }
        }

    }
}
