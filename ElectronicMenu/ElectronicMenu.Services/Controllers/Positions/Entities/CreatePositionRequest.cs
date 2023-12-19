using ElectronicMenu.Services.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ElectronicMenu.Services.Controllers.Positions.Entities
{
    public class CreatePositionRequest : IValidatableObject
    {
        [Required]
        [MinLength(1)]
        public string PositionName { get; set; }
        
        public string ImgLink { get; set; }
        
        [Required]
        [Positive]
        public float Price { get; set; }

        [Required]
        [Positive]
        public float Weight { get; set; }

        [Required]
        [Positive]
        public float Calories { get; set; }

        public int IsVegan { get; set; }

        public string Ingridients { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
