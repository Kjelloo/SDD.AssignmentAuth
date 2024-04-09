using Microsoft.EntityFrameworkCore;
using SSD.Assignment.Authorization.WebAPI.Model;

namespace SSD.Assignment.Authorization.WebAPI.Repository.DbContext;

public class NewsDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
}