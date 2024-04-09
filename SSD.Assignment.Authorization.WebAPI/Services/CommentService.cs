using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repositories.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repo;

    public CommentService(ICommentRepository repo)
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

    public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
    {
        var comments = _repo.GetCommentsByArticleId(articleId);
        
        if (comments == null || !comments.Any())
            throw new Exception("No comments found.");

        return comments;
    }

    public IEnumerable<Comment> GetCommentsByUserId(int userId)
    {
        var comments = _repo.GetCommentByUserId(userId);
        
        if (comments == null || !comments.Any())
            throw new Exception("No comments found.");

        return comments;
    }
}