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
        private readonly SqlSugarClient db;
        public FlowMeterService()
        {
            db = DatabaseService.GetClient();
        }
        public List<FlowMeter> GetAllFlowMeters()
        {
            using (db)
            {
                return db.Queryable<FlowMeter>().ToList();
            }
           
        }

        public FlowMeter GetFlowMeterById(int id)
        {
            using (db)
            {
                return db.Queryable<FlowMeter>().Where(u => u.CargoId == id).First();
            }
            
        }

        public void AddFlowMeter(FlowMeter FlowMeter)
        {
            using (db)
            {
                db.Insertable(FlowMeter).ExecuteCommand();
            }
            
        }

        public void UpdateFlowMeter(FlowMeter FlowMeter)
        {
            using (db)
            {
                db.Updateable(FlowMeter).ExecuteCommand();
            }
           
        }

        public void DeleteFlowMeter(int id)
        {
            using (db)
            {
                db.Deleteable<FlowMeter>().Where(u => u.CargoId == id).ExecuteCommand();
            }
            
        }
    }
}
