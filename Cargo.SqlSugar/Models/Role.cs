using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.SqlSugar.Models
{
    // <summary>
    /// 角色表
    /// </summary>
    [SugarTable("Role", "角色表")]
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleId { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]
        public string? RoleName { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? RoleKey { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)",ColumnDescription =("角色状态"))]
        public string? Status { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
