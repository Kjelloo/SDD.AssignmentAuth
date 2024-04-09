using Microsoft.EntityFrameworkCore;
using SSD.Assignment.Authorization.WebAPI.Model;

namespace SSD.Assignment.Authorization.WebAPI.Repository.DbContext;

public class NewsDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().HasKey(entity => entity.Id);
        modelBuilder.Entity<User>().HasKey(entity => entity.Id);
        modelBuilder.Entity<Article>().HasKey(entity => entity.Id);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
}