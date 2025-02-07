using FluentValidation;

namespace ExpenseTrackerAPI.Services.Dtos
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator() {
            RuleFor(user => user.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(user => user.OverAllBudget)
                .GreaterThan(0).WithMessage("Overall budget must be greater than zero.");
        }
    }
}
