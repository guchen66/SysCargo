
namespace Cargo.SqlSugar.Models
{
    [SugarTable("FlowMeter", "物资流水表")]//安全级别高，只创建，不修改和删除
    public class FlowMeter
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int CargoId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string? CargoName { get; set; }


        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public float Number { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(256)", IsNullable = true)]
        public int Tag { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? UserName { get; set; }
    }
}
