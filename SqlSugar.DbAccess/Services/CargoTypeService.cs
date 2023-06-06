using SqlSugar.DbAccess.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Services
{
    public class CargoTypeService
    {
        private readonly SqlSugarClient db;
        public CargoTypeService()
        {
            db = DatabaseService.GetClient();
        }
        public Cargo GetCargoById(int id)
        {
            using (db)
            {
                return db.Queryable<Cargo>().Where(u => u.Id == id).First();
            }
           
        }

        public void AddCargo(Cargo cargo)
        {
            using (db)
            {
                db.Insertable(cargo).ExecuteCommand();
            }
        }

        public void UpdateCargo(Cargo Cargo)
        {
            using (db)
            {
                db.Updateable(Cargo).ExecuteCommand();
            }
            
        }

        public void DeleteCargo(int id)
        {
            using (db)
            {
                db.Deleteable<Cargo>().Where(u => u.Id == id).ExecuteCommand();
            }
            
        }
    }
}
