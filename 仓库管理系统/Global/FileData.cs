using Microsoft.Win32;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统.Global
{
    public class FileData
    {

        /// <summary>
        /// list转化为table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }


        public static void ReaderData()
        {
            /* StreamReader sr2 = new StreamReader(@"c:\temp\utf-8.txt", Encoding.UTF8);//第二种

             FileStream fs = new FileStream(@"C:\temp\utf-8.txt", FileMode.Open, FileAccess.Read, FileShare.None); //初始化FileStream
             StreamReader sr3 = new StreamReader(fs); //第三种
             StreamReader sr4 = new StreamReader(fs, Encoding.UTF8);//第四种

             FileInfo myFile = new FileInfo(@"C:\temp\utf-8.txt"); //初始化FileInfo
                                                                   // OpenText 创建一个UTF-8 编码的StreamReader对象 
             StreamReader sr5 = myFile.OpenText();//第五种
             StreamReader sr6 = File.OpenText(@"C:\temp\utf-8.txt");//第六种*/
            StreamReader sr = new StreamReader(@"E:\VS Workspace\TestSample\b\www.txt"); //第一种
            // 读一行 
            string nextLine = sr.ReadLine();
            Console.WriteLine(nextLine);
            // 读一个字符 
            int nextChar = sr.Read();
            // 读100个字符 
            int nChars = 100;
            char[] charArray = new char[nChars];
            int nCharsRead = sr.Read(charArray, 0, nChars);
            // 全部读完 
            string restOfStream = sr.ReadToEnd();
            Console.WriteLine(restOfStream);
            //使用完StreamReader之后，关闭它：
            sr.Close();
        }
        public static void WriteData()
        {
            // 如果文件不存在，创建文件； 如果存在，覆盖文件 
            StreamWriter sw = new StreamWriter(@"c:\temp\utf-8.txt"); //第一种
                                                                      // 也可以指定编码方式 
            /*                                                             // true 是 append text, false 为覆盖原文件 
              StreamWriter sw2 = new StreamWriter(@"c:\temp\utf-8.txt", true, Encoding.UTF8);//第二种

              // FileMode.CreateNew: 如果文件不存在，创建文件；如果文件已经存在，抛出异常 
              FileStream fs = new FileStream(@"C:\temp\utf-8.txt", FileMode.CreateNew, FileAccess.Write, FileShare.Read);
              // UTF-8 为默认编码 
              StreamWriter sw3 = new StreamWriter(fs); //第三种
              StreamWriter sw4 = new StreamWriter(fs, Encoding.UTF8);//第四种

              // 如果文件不存在，创建文件； 如果存在，覆盖文件 
              FileInfo myFile = new FileInfo(@"C:\temp\utf-8.txt");
              StreamReader sr6 = File.CreateText(@"C:\temp\utf-8.txt");//第五种
              StreamWriter sw5 = myFile.CreateText();//第六种*/

            // 写一个字符            
            sw.Write('a');

            // 写一个字符数组 
            char[] charArray = new char[100];
            // initialize these characters 
            sw.Write(charArray);

            // 写一个字符数组的一部分 
            sw.Write(charArray, 10, 15);

            //使用完后关闭它
            sw.Close();
        }



    }

    public class MD5Encrypt
    {
        private MD5 md5;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5Encrypt()
        {
            md5 = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// 从字符串中获取散列值
        /// </summary>
        /// <param name="str">要计算散列值的字符串</param>
        /// <returns></returns>
        public string GetMD5FromString(string str)
        {
            byte[] toCompute = Encoding.Unicode.GetBytes(str);
            byte[] hashed = md5.ComputeHash(toCompute, 0, toCompute.Length);
            return Encoding.ASCII.GetString(hashed);
        }
        /// <summary>
        /// 根据文件来计算散列值
        /// </summary>
        /// <param name="filePath">要计算散列值的文件路径</param>
        /// <returns></returns>
        public string GetMD5FromFile(string filePath)
        {
            bool isExist = File.Exists(filePath);
            if (isExist)//如果文件存在
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, Encoding.Unicode);
                string str = reader.ReadToEnd();
                byte[] toHash = Encoding.Unicode.GetBytes(str);
                byte[] hashed = md5.ComputeHash(toHash, 0, toHash.Length);
                stream.Close();
                return Encoding.ASCII.GetString(hashed);
            }
            else//文件不存在
            {
                throw new FileNotFoundException("File not found!");
            }
        }

        /// <summary>
        /// 使用MD5加盐加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetMD5Provider(string source,string salt,[CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
           
            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source+salt));
                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            return result;
        }
    }
}
