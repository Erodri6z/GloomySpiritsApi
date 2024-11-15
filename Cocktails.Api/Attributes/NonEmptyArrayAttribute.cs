using System;
using System.ComponentModel.DataAnnotations;

namespace Cocktails.Api.Attributes;

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class NonEmptyArrayAttribute : ValidationAttribute
  {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      if (value is Array array && array.Length == 0)
      {
        return new ValidationResult($"The {validationContext.DisplayName} field cannot be an empty array.");
      }
      return ValidationResult.Success;
    }
  }
  


