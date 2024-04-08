using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repo;

    public UserService(IRepository<User> repo)
    {
        _repo = repo;
    }

    public User Add(User entity)
    {
        if (entity.Username == null || entity.Password == null)
            throw new ArgumentException("Missing required fields.");
        
        return _repo.Add(entity);
    }

    public IEnumerable<User> GetAll()
    {
        return _repo.GetAll();
    }

    public User Get(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Get(id);
    }

    public User Update(User entity)
    {
        if (entity.Username == null || entity.Password == null)
            throw new ArgumentException("Missing required fields.");
        
        return _repo.Update(entity);
    }

    public User Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Delete(id);
    }

    public User GetByUsername(string username)
    {
        if (username == null)
            throw new ArgumentException("Invalid username.");
        
        return _repo.GetAll().FirstOrDefault(u => u.Username == username);
    }
}