
namespace Cargo.SqlSugar.Repositorys
{
    public class ISqlSugarRepository<T> : SimpleClient<T> where T : class, new()
    {
        public ISqlSugarRepository()
        {

            //.NET自带IOC ,如果用构造函数注入多库模式下切换仓储功能不能自动换库可以用注入多个仓储解决
            //base.Context = IUserService.BuildServiceProvider().GetService<ISqlSugarClient>();

            //Furion:    base.Context=App.GetService<ISqlSugarClient>();
            //Furion脚手架: base.Context=DbContext.Instance
            //SqlSugar.Ioc: base.Context=DbScoped.SugarScope; 
            //手动去赋值:  base.Context=DbHelper.GetDbInstance()   
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public List<T> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
            return null;
        }
    }
}
