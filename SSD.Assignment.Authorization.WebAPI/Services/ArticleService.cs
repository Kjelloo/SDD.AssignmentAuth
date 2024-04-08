using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Services;

public class ArticleService : IService<Article>
{
    private readonly IRepository<Article> _repo;

    public ArticleService(IRepository<Article> repo)
    {
        _repo = repo;
    }

    public Article Add(Article entity)
    {
        if (string.IsNullOrEmpty(entity.Title))
            throw new ArgumentException("Title cannot be empty.");
        
        if (string.IsNullOrEmpty(entity.Content))
            throw new ArgumentException("Content cannot be empty.");
        
        if (entity.UserId <= 0)
            throw new ArgumentException("Invalid user ID.");
        
        return _repo.Add(entity);
    }

    public IEnumerable<Article> GetAll()
    {
        return _repo.GetAll();
    }

    public Article Get(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Get(id);
    }

    public Article Update(Article entity)
    {
        if (entity.Id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        if (string.IsNullOrEmpty(entity.Title))
            throw new ArgumentException("Title cannot be empty.");
        
        if (string.IsNullOrEmpty(entity.Content))
            throw new ArgumentException("Content cannot be empty.");
        
        return _repo.Update(entity);
    }

    public Article Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Delete(id);
    }
}