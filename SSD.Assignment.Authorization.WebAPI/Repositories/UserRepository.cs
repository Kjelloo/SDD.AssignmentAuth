using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly NewsDbContext _context;

    public UserRepository(NewsDbContext context)
    {
        _context = context;
    }

    public User Add(User entity)
    {
        var user = _context.Users.Add(entity);
        _context.SaveChanges();

        return user.Entity;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User Get(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("user not found");
    }

    public User Update(User entity)
    {
        var user = _context.Users.Update(entity);
        _context.SaveChanges();

        return user.Entity;
    }

    public User Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        _context.Users.Remove(user);
        _context.SaveChanges();

        return user;
    }
}