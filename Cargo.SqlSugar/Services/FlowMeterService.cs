
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    public class FlowMeterService
    {
        private readonly BaseRepository<FlowMeter> db;
        public FlowMeterService()
        {
            db = new BaseRepository<FlowMeter>();
        }
        public List<FlowMeter> GetAllFlowMeters()
        {
            return db.GetList();
        }

        public FlowMeter GetFlowMeterById(int id)
        {
           return db.GetById(id);
        }

        public void AddFlowMeter(FlowMeter FlowMeter)
        {
          db.Insert(FlowMeter);
        }

        public void UpdateFlowMeter(FlowMeter FlowMeter)
        {
            db.Delete(FlowMeter);
        }

        public void DeleteFlowMeter(int id)
        {
           db.DeleteById(id);
        }
    }
}
