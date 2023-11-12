using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("positions_in_orders")]
    public class PositionInOrderEntity : BaseEntity
    {
        public int PositionCount { get; set; }

        public int PositionId { get; set; }
        public PositionEntity Position { get; set; }

        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }

    }
}
