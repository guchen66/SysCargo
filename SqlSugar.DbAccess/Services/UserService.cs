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
        private readonly SqlSugarClient _db;

        public UserService(DatabaseService databaseService)
        {
            _db = DatabaseService.CreateClient();
        }
        public List<User> GetAllUsers()
        {
            return _db.Queryable<User>().ToList();
        }

        public User GetUserById(int id)
        {
            return _db.Queryable<User>().Where(u => u.Id == id).First();
           
        }

        public bool AddUser(User User)
        {
           
            if (_db.Insertable(User).ExecuteCommand()>0)
            {
                return true;
            }
            return false;
        }

        public void UpdateUser(User User)
        {
            _db.Updateable(User).ExecuteCommand();
        }

        public void DeleteUser(int id)
        {
            _db.Deleteable<User>().Where(u => u.Id == id).ExecuteCommand();
        }
    }
}
