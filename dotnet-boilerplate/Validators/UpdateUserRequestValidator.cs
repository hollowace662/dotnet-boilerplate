using dotnet_boilerplate.DTO;
using FluentValidation;

namespace dotnet_boilerplate.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequestDTO>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x)
                .Must(dto =>
                    !string.IsNullOrEmpty(dto.Username)
                    || !string.IsNullOrEmpty(dto.Email)
                    || dto.RoleIds != null
                )
                .WithMessage("At least one of Username, Email, or RoleIds must be provided.");

            When(
                x => x.Username != null,
                () =>
                {
                    RuleFor(x => x.Username)
                        .NotEmpty()
                        .WithMessage("Username cannot be empty when provided.")
                        .MinimumLength(3)
                        .WithMessage("Username must be at least 3 characters long.")
                        .MaximumLength(50)
                        .WithMessage("Username must not exceed 50 characters.");
                }
            );

            When(
                x => x.Email != null,
                () =>
                {
                    RuleFor(x => x.Email)
                        .NotEmpty()
                        .WithMessage("Email cannot be empty when provided.")
                        .EmailAddress()
                        .WithMessage("Email must be a valid email address.");
                }
            );

            When(
                x => x.RoleIds != null,
                () =>
                {
                    RuleFor(x => x.RoleIds)
                        .NotEmpty()
                        .WithMessage("At least one role must be assigned to the user.");
                }
            );
        }
    }
}
