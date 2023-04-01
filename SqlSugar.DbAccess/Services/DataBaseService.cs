using SqlSugar.DbAccess.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Services
{
    public class DatabaseService
    {
        public static SqlSugarClient CreateClient()
        {
            using (var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            }))
            {
                return db;
            }
        }
    }
}
