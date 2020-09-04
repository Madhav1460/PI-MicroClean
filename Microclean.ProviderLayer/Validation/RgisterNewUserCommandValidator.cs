using FluentValidation;
using Microclean.CommandQueryLayer.Commands;

namespace Microclean.ProviderLayer.Validation
{
    public class RgisterNewUserCommandValidator : AbstractValidator<FranchiseeItSelfRegistrationCommand>
    {
        public RgisterNewUserCommandValidator()
        {
            RuleFor(expression: t => t.FirstName).NotEmpty();
        }
    }
}
