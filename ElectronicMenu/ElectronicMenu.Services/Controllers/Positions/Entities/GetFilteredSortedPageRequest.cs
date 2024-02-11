using ElectronicMenu.Services.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ElectronicMenu.Services.Controllers.Positions.Entities
{
    public class GetFilteredSortedPageRequest : IValidatableObject
    {
        [Required]
        [Positive]
        public int Page { get; set; }

        [Required]
        [Positive]
        public int PageSize { get; set; }

        [Required]
        public string SortField { get; set; }

        [Required]
        public bool SortDirection { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxWeight { get; set; }
        public float? MinWeight { get; set; }
        public float? MaxCalories { get; set; }
        public float? MinCalories { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
