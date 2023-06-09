﻿using SqlSugar.DbAccess.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Db
{
    public class CodeFirst
    {
        #region  通过代码创建数据库和表
        public static void GreateDbAndTableByCode()
        {

            //代码先行，通过代码创建数据库和表
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = "Data Source=xxx;" +
                                    "Initial Catalog=xxx;" +
                                    //"Integrated Security=True"+集成验证
                                    "User ID=xxx;" +
                                    "Password=xxx",
                InitKeyType = InitKeyType.Attribute,//释放事务
                IsAutoCloseConnection = true//读取主键自增
            });

            //CodeFirst代码先行，不用关心数据库，只需要写后台代码，需要什么对象就定义什么对象，也可以一次给生成到数据库中
            //指定哪些类来生成数据库

            //反射
           //  Assembly assembly = Assembly.LoadFrom(@"E:\VS Workspace\LogicSample\2023.4\仓库管理系统\SqlSugar.DbAccess.Model\Models\");
            //IEnumerable<Type> typelist = assembly.GetTypes().Where(c => c.Namespace == "Models");

            using (db)
            {
                {
                    db.DbMaintenance.CreateDatabase();    //如果没有数据库创建
                   // db.CodeFirst.InitTables(typeof(Cargo));//通过实体类创建表
                   // db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Menu), typeof(Dept),typeof(User),typeof(SnowFlake));
                }
                //批量创建表

                Type[] types = Assembly.LoadFrom("SqlSugar.DbAccess.Model.dll").GetTypes()
                     //.Where(it=>it.FullName.Contains("Info"))//命名空间过滤
                     .ToArray();
                db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表
                /*User user = new User()
                {
                    Name = "admin",
                    CreateTime = DateTime.Now,
                    Password = "0",
                    Role = "管理员"
                };
                db.Insertable(user).ExecuteCommand();
                // 批量操作 批量插入一百条数据到User
                List<User> list = new List<User>();
                for (int i = 0; i < 100; i++)
                {
                    list.Add(new User()
                    {

                        Name = "张三"+i,
                        CreateTime = DateTime.Now,
                        Password = "pwds",
                        Role = "操作员"
                    });
                }
                db.Storageable<User>(list).ExecuteCommand();*/


                List<Cargo> list = new List<Cargo>();
                for (int i = 0; i < 20; i++)
                {
                    list.Add(new Cargo()
                    {
                        Amount = i,
                        Name = "lisi",
                        CreateTime = DateTime.Now,
                        MaterialType = "1",
                        Price = i,
                        Tag = "无备注",
                        UserId = i,
                        UserName = "张三"

                    });
                }
                db.Storageable<Cargo>(list).ExecuteCommand();



                /* db.CodeFirst.InitTables(typeof(User));
                  User user = db.Queryable<User>().First();

                 {
                     user.Name = "admin";
                     user.Password = "0";
                     user.Role = 1;
                     user.CreateTime = DateTime.Now;
                     db.Updateable(user).ExecuteCommand();

                 }*/
            }
        }


       #endregion
    }
}