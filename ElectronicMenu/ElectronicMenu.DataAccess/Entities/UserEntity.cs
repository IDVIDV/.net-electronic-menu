using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("users")]
    public class UserEntity : IdentityUser<int>, IBaseEntity
    {
        public Guid ExternalId { get; set; }
        public DateTime ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}

public class UserRoleEntity : IdentityRole<int>
{
}