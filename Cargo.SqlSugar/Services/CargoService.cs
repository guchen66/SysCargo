
namespace Cargo.SqlSugar.Services
{
    public class CargoService
    {
        private readonly SqlSugarClient db;
        public CargoService()
        {
            db = DatabaseService.GetClient();
        }
        public List<CargoModel> GetAllCargoModels()
        {
            using (db)
            {
                return db.Queryable<CargoModel>().ToList();
            }

        }

        public CargoModel GetCargoModelById(int id)
        {
            using (db)
            {
                return db.Queryable<CargoModel>().Where(u => u.Id == id).First();
            }

        }

        public void AddCargoModel(CargoModel CargoModel)
        {
            using (db)
            {
                db.Insertable(CargoModel).ExecuteCommand();
            }

        }

        public void UpdateCargoModel(CargoModel CargoModel)
        {
            using (db)
            {
                db.Updateable(CargoModel).ExecuteCommand();
            }

        }

        public void DeleteCargoModel(int id)
        {
            using (db)
            {
                db.Deleteable<CargoModel>().Where(u => u.Id == id).ExecuteCommand();
            }

        }
    }
}
