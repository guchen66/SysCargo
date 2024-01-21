
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    public class CargoTypeService
    {
        private readonly BaseRepository<CargoType> db;
        public CargoTypeService()
        {
            db =new BaseRepository<CargoType>();
        }
        public CargoType GetCargoModelById(int id)
        {
           return db.GetById(id);
        }

        public void AddCargoModel(CargoType type)
        {
            db.Insert(type);
        }

        public void UpdateCargoModel(CargoType type)
        {
            db.Update(type);
        }

        public void DeleteCargoModel(int id)
        {
            db.DeleteById(id);
        }
    }
}
