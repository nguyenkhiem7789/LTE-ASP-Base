using FluentValidation;
using LTE_ASP_Base.Models;

namespace LTE_ASP_Base.Validations
{
    public class UserValidator: AbstractValidator<UserAddRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserAddRequest request)
        {
            var validationResult = new UserValidator().Validate(request);
            return validationResult;
        }
    }
}