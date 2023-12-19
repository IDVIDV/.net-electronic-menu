using System.ComponentModel.DataAnnotations;

namespace ElectronicMenu.Services.Controllers.Users.Entities
{
    public class RegisterUserRequest : IValidatableObject
    {
        [Required]
        [MinLength(4)]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
