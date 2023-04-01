using SqlSugar.DbAccess.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Services
{
    public class FlowMeterService
    {
        private readonly SqlSugarClient _db;

        public FlowMeterService(DatabaseService databaseService)
        {
            _db = DatabaseService.CreateClient();
        }
        public List<FlowMeter> GetAllFlowMeters()
        {
            return _db.Queryable<FlowMeter>().ToList();
        }

        public FlowMeter GetFlowMeterById(int id)
        {
            return _db.Queryable<FlowMeter>().Where(u => u.CargoId == id).First();
        }

        public void AddFlowMeter(FlowMeter FlowMeter)
        {
            _db.Insertable(FlowMeter).ExecuteCommand();
        }

        public void UpdateFlowMeter(FlowMeter FlowMeter)
        {
            _db.Updateable(FlowMeter).ExecuteCommand();
        }

        public void DeleteFlowMeter(int id)
        {
            _db.Deleteable<FlowMeter>().Where(u => u.CargoId == id).ExecuteCommand();
        }
    }
}
