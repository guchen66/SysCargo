
namespace Cargo.SqlSugar.Models
{
    [SugarTable("CargoType", "物资类型表")]//安全级别高，只创建，不修改和删除
    public class CargoType
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string? Name { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(256)", IsNullable = true)]
        public string? Tag { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? UserName { get; set; }
    }
}
