
namespace Cargo.SqlSugar.Providers
{
    public class DataBaseProvider<TEntity> where TEntity : class, new()
    {
        private SqlSugarClient _db;

        public DataBaseProvider()
        {
            _db = new SqlSugarClient(new ConnectionConfig()
            {

                ConnectionString = ConnectionDbConfig.ConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute

            });
        }

        public ISugarQueryable<TEntity> Queryable
        {
            get { return _db.Queryable<TEntity>(); }
        }

        public void Insert(TEntity entity)
        {
            _db.Insertable(entity).ExecuteCommand();
        }

        public void Update(TEntity entity)
        {
            _db.Updateable(entity).ExecuteCommand();
        }

        public void Delete(TEntity entity)
        {
            _db.Deleteable(entity).ExecuteCommand();
        }

        public void SaveData(TEntity entity)
        {
            //_db.Saveable(entity).ExecuteReturnEntity(); 过时
            _db.Storageable(entity).ExecuteCommand();
        }

    }

}
