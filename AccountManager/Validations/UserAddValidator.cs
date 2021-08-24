using AccountCommands.Commands;
using FluentValidation;

namespace AccountManager.Validations
{
    public class UserAddValidator: AbstractValidator<UserAddCommand>
    {
        public UserAddValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserAddCommand command)
        {
            var validationResult = new UserAddValidator().Validate(command);
            return validationResult;
        }
    }

    public class UserChangeValidator : AbstractValidator<UserChangeCommand>
    {
        public UserChangeValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserChangeCommand command)
        {
            var validationResult = new UserChangeValidator().Validate(command);
            return validationResult;
        }
    }
}