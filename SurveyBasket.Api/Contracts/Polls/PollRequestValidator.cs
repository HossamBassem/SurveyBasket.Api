using FluentValidation;

namespace SurveyBasket.Api.Contracts.Polls
{
    public class LoginRequestValidator :AbstractValidator<PollRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(x => x.Summary)
                .MaximumLength(1500).MaximumLength(1500).WithMessage("Description must not exceed 1500 characters.");

            RuleFor(x => x.StartsAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.EndsAt)
                .NotEmpty();
            RuleFor(x => x)
                .Must(HasvalidDates)
                .WithName(nameof(PollRequest.EndsAt))
                .WithMessage("{PropertyName} must be greater than or equal to StartsAt.");
                


        }
        public bool HasvalidDates(PollRequest pollRequest)
        {
            return pollRequest.EndsAt >= pollRequest.StartsAt;
        }

    }
}
