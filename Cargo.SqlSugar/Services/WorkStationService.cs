
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    /// <summary>
    /// 工位Service
    /// </summary>
    public class WorkStationService
    {
        private readonly BaseRepository<WorkPlace> db;
        public WorkStationService()
        {
            db =new BaseRepository<WorkPlace>();
        }
        /// <summary>
        /// 查询所有工位
        /// </summary>
        /// <returns></returns>
        public List<WorkPlace> GetAllWorkPlaces()
        {
            return db.GetList();
        }

        /// <summary>
        /// 查询所有工位名称
        /// </summary>
        /// <returns></returns>
        public List<WorkPlace> GetAllWorkPlacesNames()
        {
            return db.GetList();
        }
        /// <summary>
        /// 按照工位Id查询工位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkPlace GetWorkPlaceById(int id)
        {
            return db.GetById(id);
        }

        /// <summary>
        /// 按照工位名称查询工位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkPlace GetWorkPlaceByName(string name)
        {
            return db.GetSingle(x=>x.WorkPlaceName==name);
        }

        public bool AddWorkPlace(WorkPlace workPlace)
        {
            if (db.Insert(workPlace))
            {
                return true;
            }
            return db.Insert(workPlace);
        }

        public void UpdateWorkPlace(WorkPlace workPlace)
        {
            db.Update(workPlace);
        }

        public void DeleteWorkPlace(int id)
        {
            /* if (IsWorkInProgress)
             {
                 // 现场正在工作，不能删除工位
                 MessageBox.Show("现场正在工作，不能删除工位。");
                 return;
             }
 */
            /*   var result = MessageBox.Show("是否连同对应工序一起删除？", "删除工位", MessageBoxButton.YesNo);
               if (result == MessageBoxResult.Yes)
               {

               }*/

           /* using (db)
            {
                db.Ado.BeginTran(); // 开启事务
                try
                {
                    // 删除工位
                    db.Deleteable<WorkPlace>().Where(u => u.WorkPlaceId == id).ExecuteCommand();

                    // 删除关联的工序
                    db.Deleteable<ProcessModel>().Where(u => u.WorkPlaceId == id).ExecuteCommand();

                    db.Ado.CommitTran(); // 提交事务
                }
                catch (Exception)
                {
                    db.Ado.RollbackTran();// 回滚事务
                    throw; // 抛出异常
                }
            }*/
        }
    }

}
