using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Db
{
    public class SugarGlobal
    {
       
        public static void Initialized()
        {

            // DbFirst.GetModelByDb();
             CodeFirst.GreateDbAndTableByCode(); 
           
        }
    }
}
