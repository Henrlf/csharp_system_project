using FluentValidation;
using Franco.Sentry.Application.Auth.Query;

namespace Franco.Sentry.Application.Auth.Validation;

public class UserLoginValidation : AbstractValidator<UserLoginQuery>
{
    public UserLoginValidation()
    {
        ValidateUsername();
        ValidatePassword();
    }

    private void ValidateUsername()
    {
        RuleFor(c => c.Username)
            .NotEmpty()
            .WithName("username")
            .WithMessage("Username is required!");
    }

    private void ValidatePassword()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithName("password")
            .WithMessage("Password is required!");
    }
}