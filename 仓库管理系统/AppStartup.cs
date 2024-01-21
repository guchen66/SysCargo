using NewLife.Configuration;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统
{
    public static class AppStartup
    {
        public static void AddSqlSugar()
        {
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                ConnectionString = GetJsonData(),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }
        /// <summary>
        /// 使用NewLife读取Json
        /// </summary>
        /// <returns></returns>
        public static string GetJsonData()
        {
            string result = "";
            var provider = new JsonConfigProvider()
            {
                FileName = "AppConfig.json"
            };
            result = provider.GetSection("SqlConnection:ConnectionString").Value;
            return result;
        }
    }
}
