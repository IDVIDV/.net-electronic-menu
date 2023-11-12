using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
