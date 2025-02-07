using FluentValidation;

namespace ExpenseTrackerAPI.Services.Dtos
{
    public class ExpenseDtoValidator : AbstractValidator<ExpenseDTO>
    {
        public ExpenseDtoValidator()
        {
            RuleFor(e => e.Amount)
                .GreaterThan(0).WithMessage("Expense amount must be greater than zero.");

            RuleFor(e => e.Date)
                .NotEmpty().WithMessage("Expense date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Expense date cannot be in the future.");

            RuleFor(e => e.CategoryId)
                .GreaterThan(0).WithMessage("Category ID must be valid.");

            RuleFor(e => e.UserId)
                .GreaterThan(0).WithMessage("User ID must be valid.");
        }
    }
}
