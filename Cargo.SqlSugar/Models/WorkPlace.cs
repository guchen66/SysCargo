
namespace Cargo.SqlSugar.Models
{
    [SugarTable("WorkPlace", "工位表")]
    public class WorkPlace
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int Id { get; set; }

        public int WorkPlaceId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string WorkPlaceName { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsWorkInProgress { get; set; }
        /// <summary>
        ///一对多的使用，一个工位多个工序
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ProcessModel.WorkPlaceName))]
        public List<ProcessModel> ProcessModelList { get; set; }
    }
}
