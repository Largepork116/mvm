using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class CredencialesViewModelValidator : AbstractValidator<CredencialesViewModel>
    {
        public CredencialesViewModelValidator()
        {
            RuleFor(vm => vm.Username)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Password)
                .NotEmpty().WithMessage("No puede ser vacio");
        }
    }
}
