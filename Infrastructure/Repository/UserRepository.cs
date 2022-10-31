using GlobalErrorApp.Models;
using GlobalErrorApp.UseCase;

namespace GlobalErrorApp.Infrastructure.Repository;

public class UserRepository
{
    private readonly List<User> _usersDb = new(){
        new(1,"jorge"),
        new(2,"clinton")
    };
    public User? FindById(int id)
    {
        return _usersDb.FirstOrDefault(u => u.Id == id);
    }

    public void Add(AddUserCommand command)
    {
        _usersDb.Add(new User(Id: _usersDb.Count + 1, Name: command.Name));
    }
}