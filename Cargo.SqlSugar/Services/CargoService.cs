
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    public class CargoService
    {
        private readonly BaseRepository<CargoModel> db;
        public CargoService()
        {
            db = new BaseRepository<CargoModel>();
        }
        public List<CargoModel> GetAllCargoModels()
        {
            return db.GetList();
        }

        public CargoModel GetCargoModelById(int id)
        {
           return db.GetById(id);
        }

        public void AddCargoModel(CargoModel CargoModel)
        {
           db.Insert(CargoModel);
        }

        public void UpdateCargoModel(CargoModel CargoModel)
        {
           db.Update(CargoModel);
        }

        public void DeleteCargoModel(int id)
        {
          db.DeleteById(id);
        }
    }
}
