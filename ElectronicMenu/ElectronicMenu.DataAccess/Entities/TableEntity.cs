using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("tables")]
    public class TableEntity : BaseEntity
    {
        public int Capacity { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
