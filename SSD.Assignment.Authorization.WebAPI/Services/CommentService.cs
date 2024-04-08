using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Services;

public class CommentService : IService<Comment>
{
    private readonly IRepository<Comment> _repo;

    public CommentService(IRepository<Comment> repo)
    {
        _repo = repo;
    }

    public Comment Add(Comment entity)
    {
        if (string.IsNullOrEmpty(entity.Content))
            throw new ArgumentException("Comment cannot be empty.");
        
        if (entity.ArticleId <= 0)
            throw new ArgumentException("Invalid news ID.");
        
        if (entity.UserId <= 0)
            throw new ArgumentException("Invalid user ID.");
        
        return _repo.Add(entity);
    }

    public IEnumerable<Comment> GetAll()
    {
        return _repo.GetAll();
    }

    public Comment Get(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Get(id);
    }

    public Comment Update(Comment entity)
    {
        if (string.IsNullOrEmpty(entity.Content))
            throw new ArgumentException("Comment cannot be empty.");
        
        if (entity.Id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Update(entity);
    }

    public Comment Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID.");
        
        return _repo.Delete(id);
    }
}