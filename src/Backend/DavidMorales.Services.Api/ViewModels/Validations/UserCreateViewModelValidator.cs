using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class UserCreateViewModelValidator : AbstractValidator<UserCreateViewModel>
    {
        public UserCreateViewModelValidator()
        {
            RuleFor(vm => vm.Person.Name)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Person.LastName)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Person.Phone)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Person.CompanyId)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Email)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Role)
                .NotEmpty().WithMessage("No puede ser vacio");
        }
    }
}
