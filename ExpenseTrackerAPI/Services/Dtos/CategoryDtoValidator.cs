using FluentValidation;

namespace ExpenseTrackerAPI.Services.Dtos
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {

        public CategoryDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(3, 50).WithMessage("Category name must be between 3 and 50 characters.");

            RuleFor(c => c.Budget)
                .GreaterThan(0).WithMessage("Category budget must be greater than zero.");

           
        }

    }
}
