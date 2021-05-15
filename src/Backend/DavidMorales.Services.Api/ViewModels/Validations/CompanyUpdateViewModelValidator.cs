using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class CompanyUpdateViewModelValidator : AbstractValidator<CompanyUpdateViewModel>
    {
        public CompanyUpdateViewModelValidator()
        {
            RuleFor(vm => vm.Name)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.CompanyId)
                .NotEmpty().WithMessage("No puede ser vacio")
                .GreaterThan(0).WithMessage("Valor no válido");
        }
    }
}
