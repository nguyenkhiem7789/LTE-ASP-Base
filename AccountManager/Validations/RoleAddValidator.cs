using AccountCommands.Commands;
using FluentValidation;

namespace AccountManager.Validations
{
    public class RoleAddValidator: AbstractValidator<RoleAddCommand>
    {
        public RoleAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleAddCommand command)
        {
            var validationResult = new RoleAddValidator().Validate(command);
            return validationResult;
        }
    }
}