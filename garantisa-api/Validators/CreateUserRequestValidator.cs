using FluentValidation;
using garantisa.DTO;

namespace garantisa.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDTO>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Length(3, 50)
                .WithMessage("Username must be between 3 and 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email is required.");

            RuleFor(x => x.RoleIds)
                .NotNull()
                .WithMessage("Role IDs cannot be null.")
                .NotEmpty()
                .WithMessage("At least one role is required.");
        }
    }
}
