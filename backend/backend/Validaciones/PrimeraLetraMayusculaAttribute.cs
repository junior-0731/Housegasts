using System.ComponentModel.DataAnnotations;

namespace pelicualasAPI.validaciones
{
    // Heredar de la clase ValidationAttribute
    public class PrimeraLetraMayusculaAtribute: ValidationAttribute

    {
        // 
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if ((value is null || string.IsNullOrWhiteSpace(value.ToString())))
            {
                return ValidationResult.Success;
            }
            var primeraLetra = value.ToString()[0].ToString();
            if (primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayuscula");
            }
            return ValidationResult.Success;


        }

        }
}
