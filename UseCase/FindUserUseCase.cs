using GlobalErrorApp.Infrastructure.Repository;
using GlobalErrorApp.Models;

namespace GlobalErrorApp.UseCase;

public interface IFindUserUseCase
{
    User? FindById(int id);
}

public class FindUserUseCase : IFindUserUseCase
{
    public User? FindById(int id)
    {
        return new UserRepository().FindById(id);
    }
}