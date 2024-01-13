namespace Cargo.System.Services
{
    public class CargoTypeService
    {
        private readonly SqlSugarClient db;
        public CargoTypeService()
        {
            db = DatabaseService.GetClient();
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
