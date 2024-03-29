﻿namespace Cargo.SqlSugar.Models
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [SugarTable("User", "用户表")]//, IsDisabledUpdateAll = true安全级别高，只创建，不修改和删除
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//主键自增
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)")]//自定义情况Length不要设置
        public string? Name { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(32)")]
        public string? Password { get; set; }

        public int RoleId { get; set; }

        public DateTime CreateTime { get; set; }

        [Navigate(NavigateType.OneToOne,nameof(RoleId))]
        public Role Role { get; set; }
    }
}