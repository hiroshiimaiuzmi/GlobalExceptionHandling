using GlobalErrorApp.Infrastructure.Repository;

namespace GlobalErrorApp.UseCase;

public interface IAddUserUseCase
{
    void Add(AddUserCommand command);
}

public class AddUserUseCase : IAddUserUseCase
{
    public void Add(AddUserCommand command)
    {
        new UserRepository().Add(command);
    }
}