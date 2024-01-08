
namespace Cargo.SqlSugar.Services
{
    public class ProcessService
    {
        private readonly SqlSugarClient db;
        public ProcessService()
        {
            db = DatabaseService.GetClient();
        }
        /// <summary>
        /// 查询所有工序信息
        /// 查询到用户名称
        /// </summary>
        /// <returns></returns>
        public List<ProcessModel> GetAllProcessModels()
        {
            using (db)
            {
                return db.Queryable<ProcessModel>().ToList();
            }

        }

        public ProcessModel GetProcessModelById(int id)
        {
            using (db)
            {
                return db.Queryable<ProcessModel>().Where(u => u.Id == id).First();
            }
        }

        public bool AddProcessModel(ProcessModel processModel)
        {
            using (db)
            {
                if (db.Insertable(processModel).ExecuteCommand() > 0)
                {
                    return true;
                }
                return false;
            }

        }

        public void UpdateProcessModel(ProcessModel processModel)
        {
            using (db)
            {
                db.Updateable(processModel).ExecuteCommand();
            }

        }

        public void DeleteProcessModel(int id)
        {
            using (db)
            {
                db.Deleteable<ProcessModel>().Where(u => u.Id == id).ExecuteCommand();
            }
        }

        /// <summary>
        /// 删除此工序的所有工位
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorkStationList(int id)
        {
            using (db)
            {
                db.Deleteable<ProcessModel>().Where(u => u.WorkPlaceId == id).ExecuteCommand();
            }
        }
    }
}
