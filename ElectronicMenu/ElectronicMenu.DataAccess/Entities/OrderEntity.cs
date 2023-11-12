using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("orders")]
    public class OrderEntity : BaseEntity
    {
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int TableId { get; set; }
        public TableEntity Table { get; set; }

        public virtual ICollection<PositionInOrderEntity> PositionsInOrder { get; set; }
    }
}
