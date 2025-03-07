using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Validation
{
    public class ValidateDueDate
    {
        public static ValidationResult? ValidateDate(DateTime dueDate, ValidationContext context)
        {
            if (dueDate < DateTime.Today)
            {
                return new ValidationResult("Due date must be today or in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
