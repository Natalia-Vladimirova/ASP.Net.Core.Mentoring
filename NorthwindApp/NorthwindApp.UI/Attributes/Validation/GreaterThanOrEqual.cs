using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NorthwindApp.UI.Attributes.Validation
{
    public class GreaterThanOrEqual : ValidationAttribute, IClientModelValidator
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

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-greaterThanOrEqual", ErrorMessage);
            MergeAttribute(context.Attributes, "data-val-greaterThanOrEqual-minValue", _minValue.ToString());
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);

            return true;
        }
    }
}
