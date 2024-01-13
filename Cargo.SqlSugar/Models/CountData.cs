using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.SqlSugar.Models
{
    /// <summary>
    /// 数据统计表
    /// </summary>
    [SugarTable("CountData", "统计数据表")]
    public class CountData
    {
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string? CountName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? MaxValue { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? MinValue { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? AgeValue { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? TotalValue { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
