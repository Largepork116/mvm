using FluentValidation;

namespace DavidMorales.Services.Api.ViewModels.Validations
{
    public class UserEditViewModelValidator : AbstractValidator<UserUpdateViewModel>
    {
        public UserEditViewModelValidator()
        {
            RuleFor(vm => vm.Email)
                .NotEmpty().WithMessage("No puede ser vacio");

            RuleFor(vm => vm.Role)
                .NotEmpty().WithMessage("No puede ser vacio");
        }
    }
}
