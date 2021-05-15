using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class PersonUpdateViewModelValidator : AbstractValidator<PersonUpdateViewModel>
    {
        public PersonUpdateViewModelValidator()
        {
            RuleFor(vm => vm.PersonId)
                .NotEmpty().WithMessage("No puede ser vacio")
                .GreaterThan(0).WithMessage("Valor no válido");

            RuleFor(vm => vm.Name)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.LastName)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Phone)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.CompanyId)
                .NotEmpty().WithMessage("No puede ser vacio")
                .GreaterThan(0).WithMessage("Valor no válido");
        }
    }
}
