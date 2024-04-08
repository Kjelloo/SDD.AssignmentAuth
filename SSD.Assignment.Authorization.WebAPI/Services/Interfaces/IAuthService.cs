using SSD.Assignment.Authorization.WebAPI.Model;

namespace SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, User user);
    string GenerateToken (User user);
}