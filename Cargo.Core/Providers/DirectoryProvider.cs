using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Providers
{
    public static class DirectoryProvider
    {
        public static readonly IEnumerable<Type> EffectiveTypes;
        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <returns></returns>
        public static string SelectRootDirectory()
        {
            string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            return rootDirectory;
        }

        /// <summary>
        /// 通过名称获取目录
        /// </summary>
        /// <returns></returns>
        public static string SelectDirectoryByName(string resourceName)
        {
            var folder = Directory.GetCurrentDirectory(); // 获取应用程序当前工作目录
            var path = Path.Combine(folder, resourceName); // 使用 Path.Combine 组合路径
            return path;
        }
    }
}
