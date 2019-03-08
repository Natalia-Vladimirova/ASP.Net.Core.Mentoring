using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.UI.Attributes.Validation
{
    public class GreaterThanOrEqual : ValidationAttribute
    {
        private readonly int _minValue;

        public GreaterThanOrEqual(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            if (int.TryParse(value?.ToString(), out var result))
            {
                return result >= _minValue;
            }

            return false;
        }
    }
}
