using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class CompanyCreateViewModelValidator : AbstractValidator<CompanyCreateViewModel>
    {
        public CompanyCreateViewModelValidator()
        {
            RuleFor(vm => vm.Name)
                .NotEmpty().WithMessage("No puede ser vacio");
        }
    }
}
