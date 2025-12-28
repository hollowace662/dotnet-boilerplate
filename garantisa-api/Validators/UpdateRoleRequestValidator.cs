using FluentValidation;
using garantisa.DTO;

namespace garantisa.Validators
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequestDTO>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x)
                .Must(dto =>
                    !string.IsNullOrEmpty(dto.Name) || !string.IsNullOrEmpty(dto.Description)
                )
                .WithMessage("At least one of Name or Description must be provided.");

            When(
                x => x.Name != null,
                () =>
                {
                    RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithMessage("Role name cannot be empty when provided.")
                        .MinimumLength(3)
                        .WithMessage("Role name must be at least 3 characters long.")
                        .MaximumLength(50)
                        .WithMessage("Role name must not exceed 50 characters.");
                }
            );

            When(
                x => x.Description != null,
                () =>
                {
                    RuleFor(x => x.Description)
                        .NotEmpty()
                        .WithMessage("Description cannot be empty when provided.")
                        .MinimumLength(10)
                        .WithMessage("Description must be at least 10 characters long.")
                        .MaximumLength(200)
                        .WithMessage("Description must not exceed 200 characters.");
                }
            );
        }
    }
}
