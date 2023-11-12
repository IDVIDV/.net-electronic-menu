using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicMenu.DataAccess.Entities
{
    [Table("admins")]
    public class AdminEntity : BaseEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
