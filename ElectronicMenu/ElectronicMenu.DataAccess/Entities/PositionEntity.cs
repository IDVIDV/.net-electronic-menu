using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("positions")]
    public class PositionEntity : BaseEntity
    {
        public string PositionName { get; set; }
        public string ImgLink { get; set; }
        public float Price { get; set; }
        public float Weight { get; set; }
        public float Calories { get; set; }
        public int IsVegan { get; set; }
        public string Ingridients { get; set; }

        public virtual ICollection<PositionInOrderEntity> PositionInOrders { get; set; }
    }
}
