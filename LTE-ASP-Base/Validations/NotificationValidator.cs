using FluentValidation;
using LTE_ASP_Base.Models;

namespace LTE_ASP_Base.Validations
{
    public class NotificationAddValidator: AbstractValidator<NotificationAddRequest>
    {
        public NotificationAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(NotificationAddRequest request)
        {
            var validationResult = new NotificationAddValidator().Validate(request);
            return validationResult;
        }
    }
    
    public class NotificationChangeValidator: AbstractValidator<NotificationChangeRequest>
    {
        public NotificationChangeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(NotificationChangeRequest request)
        {
            var validationResult = new NotificationChangeValidator().Validate(request);
            return validationResult;
        }
    }
    
    public class NotificationGetByIdValidator: AbstractValidator<NotificationGetByIdRequest>
    {
        public NotificationGetByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Notification isn't exist!");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(NotificationGetByIdRequest request)
        {
            var validationResult = new NotificationGetByIdValidator().Validate(request);
            return validationResult;
        }
    }
}