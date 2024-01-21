
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    public class ProcessService
    {
        private readonly BaseRepository<ProcessModel> db;
        public ProcessService()
        {
            db = new BaseRepository<ProcessModel>();
        }
        /// <summary>
        /// 查询所有工序信息
        /// 查询到用户名称
        /// </summary>
        /// <returns></returns>
        public List<ProcessModel> GetAllProcessModels()
        {
            return db.GetList();
        }

        public ProcessModel GetProcessModelById(int id)
        {
            return db.GetById(id);
        }

        public bool AddProcessModel(ProcessModel processModel)
        {
           return db.Insert(processModel);

        }

        public void UpdateProcessModel(ProcessModel processModel)
        {
           db.Update(processModel);
        }

        public void DeleteProcessModel(int id)
        {
            db.DeleteById(id);
        }

        /// <summary>
        /// 删除此工序的所有工位
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorkStationList(int id)
        {
            db.Delete(x=>x.WorkPlaceId==id);//.Where(u => u.WorkPlaceId == id).ExecuteCommand();
        }
    }
}
