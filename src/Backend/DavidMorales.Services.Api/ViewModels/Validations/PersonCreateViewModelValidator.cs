using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class PersonCreateViewModelValidator : AbstractValidator<PersonCreateViewModel>
    {
        public PersonCreateViewModelValidator()
        {
            RuleFor(vm => vm.Name)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.LastName)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Phone)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.CompanyId)
                .NotEmpty().WithMessage("No puede ser vacio");
        }
    }
}
