using FluentValidation;
using NotificationCommands.Commands;

namespace NotificationManager.Validations
{
    public class NotificationAddValidator: AbstractValidator<NotificationAddCommand>
    {
        public NotificationAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(NotificationAddCommand request)
        {
            var validationResult = new NotificationAddValidator().Validate(request);
            return validationResult;
        }
    }
        
    public class NotificationChangeValidator: AbstractValidator<NotificationChangeCommand>
    {
        public NotificationChangeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(NotificationChangeCommand request)
        {
            var validationResult = new NotificationChangeValidator().Validate(request);
            return validationResult;
        }
    }
}