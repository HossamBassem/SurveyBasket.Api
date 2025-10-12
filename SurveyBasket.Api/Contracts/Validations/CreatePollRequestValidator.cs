using FluentValidation;
using SurveyBasket.Api.Contracts.Requests;

namespace SurveyBasket.Api.Contracts.Validations
{
    public class CreatePollRequestValidator :AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
