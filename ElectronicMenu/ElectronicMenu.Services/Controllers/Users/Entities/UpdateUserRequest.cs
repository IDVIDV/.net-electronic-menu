using ElectronicMenu.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace ElectronicMenu.Services.Controllers.Users.Entities
{
    public class UpdateUserRequest : IValidatableObject
    {
        [Required]
        [MinLength(4)]
        public string Login { get; set; }

        //???????
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        //Тоже что-то нужно сделать с OrderEntity
        public List<OrderEntity> Orders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
