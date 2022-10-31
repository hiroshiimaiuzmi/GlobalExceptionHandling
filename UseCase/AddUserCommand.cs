using FluentValidation;

namespace GlobalErrorApp.UseCase;

public record AddUserCommand
(
    string Name
)
{ }

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}