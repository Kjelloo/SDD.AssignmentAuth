using SSD.Assignment.Authorization.WebAPI.Model;

namespace SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

public interface IUserService : IService<User>
{
    User GetByUsername(string username);
}