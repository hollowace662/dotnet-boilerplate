using FluentValidation;
using garantisa.DTO;

namespace garantisa.Validators
{
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequestDTO>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Role name is required.")
                .MinimumLength(3)
                .WithMessage("Role name must be at least 3 characters long.")
                .MaximumLength(50)
                .WithMessage("Role name must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MinimumLength(10)
                .WithMessage("Description must be at least 10 characters long.")
                .MaximumLength(200)
                .WithMessage("Description must not exceed 200 characters.");
        }
    }
}
