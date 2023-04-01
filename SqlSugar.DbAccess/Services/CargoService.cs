using SqlSugar.DbAccess.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Services
{
    public class CargoService
    {
        private readonly SqlSugarClient _db;

        public CargoService(DatabaseService databaseService)
        {
            _db = DatabaseService.CreateClient();
        }
        public List<Cargo> GetAllCargos()
        {
            return _db.Queryable<Cargo>().ToList();
        }

        public Cargo GetCargoById(int id)
        {
            return _db.Queryable<Cargo>().Where(u => u.Id == id).First();
        }

        public void AddCargo(Cargo Cargo)
        {
            _db.Insertable(Cargo).ExecuteCommand();
        }

        public void UpdateCargo(Cargo Cargo)
        {
            _db.Updateable(Cargo).ExecuteCommand();
        }

        public void DeleteCargo(int id)
        {
            _db.Deleteable<Cargo>().Where(u => u.Id == id).ExecuteCommand();
        }
    }
}
