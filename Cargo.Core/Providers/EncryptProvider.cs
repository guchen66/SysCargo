using NewLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Providers
{
    public class EncryptProvider
    {
        private Aes aes;

        public EncryptProvider()
        {
            aes = Aes.Create();
            MyPass = SetPassword();
        }
        public byte[] SetPassword()
        {
            return Encoding.UTF8.GetBytes("NewLife"); // 设置固定的密码
        }

        private byte[] MyPass;

        public string SetAESEncrypt(string data)
        {
            var value = data.GetBytes();
            var rs = aes.Encrypt(value, MyPass).ToBase64(); // 执行加密并转为 Base64 字符串
            return rs;
        }

        public string GetAESEncrypt(string data)
        {
            byte[] encryptedBytes = data.ToBase64();
            var txt = aes.Decrypt(encryptedBytes, MyPass).ToStr(); // 执行解密并转为字符串
            return txt;
        }
    }
}
