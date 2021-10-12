using AccountReadModels;
using FluentValidation;
using LTE_ASP_Base.Models;
using LTE_ASP_Base.Shared.Requests;

namespace LTE_ASP_Base.Validations
{
    public class RoleAddValidator: AbstractValidator<RoleAddRequest>
    {
        public RoleAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleAddRequest request)
        {
            var validationResult = new RoleAddValidator().Validate(request);
            return validationResult;
        }
    }

    public class RoleChangeValidator : AbstractValidator<RoleChangeRequest>
    {
        public RoleChangeValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User is not exist.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleChangeRequest request)
        {
            var validationResult = new RoleChangeValidator().Validate(request);
            return validationResult;
        }
    }
    
    public class RoleGetByIdValidator : AbstractValidator<RoleGetByIdRequest>
    {
        public RoleGetByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User is not exist.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleGetByIdRequest request)
        {
            var validationResult = new RoleGetByIdValidator().Validate(request);
            return validationResult;
        }
    }
}