using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Common.Validation;
[AttributeUsage(AttributeTargets.Property)]
public class DateGreaterThanAttribute : ValidationAttribute
{
	private readonly string _comparisonProperty;

	public DateGreaterThanAttribute(string comparisonProperty)
	{
		_comparisonProperty = comparisonProperty;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		var currentValue = value is DateTime dt ? dt : DateTime.MinValue;

		var propertyValue = validationContext.ObjectType
				.GetProperty(_comparisonProperty)?
				.GetValue(validationContext.ObjectInstance);

		var comparisonValue = propertyValue is DateTime cv ? cv : DateTime.MinValue;

		if (currentValue < comparisonValue)
		{
			return new ValidationResult(ErrorMessage = "End date must be later than start date!");
		}

		return ValidationResult.Success;
	}
}
