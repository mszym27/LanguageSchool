using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class IntSameAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public IntSameAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (int)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

            if (currentValue == comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
