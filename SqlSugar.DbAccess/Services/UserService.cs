using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Services
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
            using (db)
            {
                return db.Queryable<User>().ToList();
            }
           
        }

        public User GetUserById(int id)
        {
            using (db)
            {
                return db.Queryable<User>().Where(u => u.Id == id).First();
            }
        }

        public bool AddUser(User User)
        {
            using (db)
            {
                if (db.Insertable(User).ExecuteCommand() > 0)
                {
                    return true;
                }
                return false;
            }
           
        }

        public void UpdateUser(User User)
        {
            using (db)
            {
                db.Updateable(User).ExecuteCommand();
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
