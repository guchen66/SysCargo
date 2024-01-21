
using Cargo.SqlSugar.Repositorys;

namespace Cargo.SqlSugar.Services
{
    public class UserService:BaseRepository<User>
    {
        private readonly BaseRepository<User> db;
        public UserService()
        {
            db =new BaseRepository<User>();
        }

        public List<User> GetUserList() 
        {
            return db.GetList();
        }
        public User GetCargoModelById(int id)
        {
            return db.GetById(id);
        }

        public void AddCargoModel(User user)
        {
            db.Insert(user);
        }

        public void UpdateCargoModel(User user)
        {
            db.Update(user);
        }

        public void DeleteCargoModel(int id)
        {
            db.DeleteById(id);
        }
    }
}
