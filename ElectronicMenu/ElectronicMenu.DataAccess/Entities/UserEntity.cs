using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("users")]
    public class UserEntity : IdentityUser<int>, IBaseEntity
    {
        public Guid ExternalId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ModificationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Login { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}

public class UserRoleEntity : IdentityRole<int>
{
}