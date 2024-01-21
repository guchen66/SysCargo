using Cargo.SqlSugar.Db;

namespace Cargo.System.Services
{
    public class DatabaseService
    {
        private static readonly Lazy<SqlSugarClient> _db = new Lazy<SqlSugarClient>(() =>
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            // 配置 SqlSugarClient 对象

            return db;
        });

        public static SqlSugarClient GetClient()
        {
            return _db.Value;
        }
    }
}
