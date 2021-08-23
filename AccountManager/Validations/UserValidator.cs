using AccountCommands.Commands;
using FluentValidation;

namespace AccountManager.Validations
{
    public class UserValidator: AbstractValidator<UserAddCommand>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserAddCommand request)
        {
            var validationResult = new UserValidator().Validate(request);
            return validationResult;
        }
    }
}