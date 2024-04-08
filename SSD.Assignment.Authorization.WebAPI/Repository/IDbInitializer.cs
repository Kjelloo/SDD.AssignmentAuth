using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;

namespace SSD.Assignment.Authorization.WebAPI.Repository;

public interface IDbInitializer
{
    void Initialize(NewsDbContext context);
}