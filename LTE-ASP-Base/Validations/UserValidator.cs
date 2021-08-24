using FluentValidation;
using LTE_ASP_Base.Models;

namespace LTE_ASP_Base.Validations
{
    public class UserAddValidator: AbstractValidator<UserAddRequest>
    {
        public UserAddValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserAddRequest request)
        {
            var validationResult = new UserAddValidator().Validate(request);
            return validationResult;
        }
    }
    
    public class UserChangeValidator: AbstractValidator<UserChangeRequest>
    {
        public UserChangeValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User is not exist.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(UserChangeRequest request)
        {
            var validationResult = new UserChangeValidator().Validate(request);
            return validationResult;
        }
    }
    
}