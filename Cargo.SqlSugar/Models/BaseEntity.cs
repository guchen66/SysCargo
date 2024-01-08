
namespace Cargo.SqlSugar.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public int BaseId { get; set; }

        public DateTime GreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
