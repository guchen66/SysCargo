
namespace Cargo.SqlSugar.Models
{
    [SugarTable("ProcessModel", "工序表")]
    public class ProcessModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int Id { get; set; }

        public int WorkPlaceId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string WorkPlaceName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string UserName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string ProcessName { get; set; }

        public DateTime CreateTime { get; set; }


        public bool IsWorkInProgress { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(UserName))]
        public User User { get; set; }
    }
}
