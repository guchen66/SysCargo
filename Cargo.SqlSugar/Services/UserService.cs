
namespace Cargo.SqlSugar.Services
{
    public class UserService
    {
        private readonly SqlSugarClient db;
        public UserService()
        {
            db = DatabaseService.GetClient();
        }
        public List<User> GetAllUsers()
        {
            return db.Queryable<User>().ToList();

        }

        public User GetUserById(int id)
        {
            using (db)
            {
                return db.Queryable<User>().Where(u => u.Id == id).First();
            }
        }

        public bool AddUser(User user)
        {
            using (db)
            {
                if (db.Insertable(user).ExecuteCommand() > 0)
                {
                    return true;
                }
                return false;
            }

        }

        public void UpdateUser(User user)
        {
            using (db)
            {
                db.Updateable(user).ExecuteCommand();
            }

        }

        public void DeleteUser(int id)
        {
            using (db)
            {
                db.Deleteable<User>().Where(u => u.Id == id).ExecuteCommand();
            }

        }
    }
}
