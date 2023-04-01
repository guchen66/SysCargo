using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.DbAccess.Model.Models
{
    [SugarTable("Cargo", "物资表")]//安全级别高，只创建，不修改和删除
    public class Cargo
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]//主键自增
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)",IsNullable =true)]//自定义情况Length不要设置
        public string? Name { get; set; }


        [SugarColumn(ColumnDataType = "Nvarchar(50)",IsNullable =true)]
        public string? MaterialType { get; set; }


        public int Amount { get; set; }

        [SugarColumn(DecimalDigits =2)]
        public decimal? Price { get; set; }


        [SugarColumn(ColumnDataType = "Nvarchar(256)", IsNullable = true)]
        public string? Tag { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? UserName { get; set; }

    }
}
