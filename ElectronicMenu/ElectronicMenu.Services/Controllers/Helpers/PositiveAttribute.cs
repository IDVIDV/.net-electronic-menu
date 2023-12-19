using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace ElectronicMenu.Services.Controllers.Helpers
{
    public class PositiveAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            double? numValue = Convert.ToDouble(value);
            if (numValue == null)
                return false;

            if (numValue > 0)    // если имя не равно admin
                return true;

            ErrorMessage = "Некорректное имя: admin";
            return false;
        }
    }
}
