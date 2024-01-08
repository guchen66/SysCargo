using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.SqlSugar.Db
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
