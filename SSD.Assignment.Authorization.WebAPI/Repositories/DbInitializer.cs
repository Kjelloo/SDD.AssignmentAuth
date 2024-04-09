using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repositories.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;

namespace SSD.Assignment.Authorization.WebAPI.Repository;

public class DbInitializer : IDbInitializer
{
    public void Initialize(NewsDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "editor",
                Password = BCrypt.Net.BCrypt.HashPassword("editor"),
                Role = Roles.Editor
            },
            new User
            {
                Id = 2,
                Username = "writer",
                Password = BCrypt.Net.BCrypt.HashPassword("writer"),
                Role = Roles.Writer
            },
            new User
            {
                Id = 3,
                Username = "writer2",
                Password = BCrypt.Net.BCrypt.HashPassword("writer2"),
                Role = Roles.Writer
            },
            new User
            {
                Id = 4,
                Username = "user",
                Password = BCrypt.Net.BCrypt.HashPassword("user"),
                Role = Roles.User
            }
        };

        var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Content = "Comment 1",
                UserId = 4,
                ArticleId = 1
            },
            new Comment
            {
                Id = 2,
                Content = "Comment 2",
                UserId = 4,
                ArticleId = 2
            }
        };
        
        var articles = new List<Article>
        {
            new Article
            {
                Id = 1,
                Title = "Article 1",
                Content = "Content 1",
                UserId = 2,
                CreatedAt = DateTime.Now
            },
            new Article
            {
                Id = 2,
                Title = "Article 2",
                Content = "Content 2",
                UserId = 3,
                CreatedAt = DateTime.Now.AddDays(-1)
            }
        };
        
        context.Users.AddRange(users);
        context.Comments.AddRange(comments);
        context.Articles.AddRange(articles);
        
        context.SaveChanges();
    }
}