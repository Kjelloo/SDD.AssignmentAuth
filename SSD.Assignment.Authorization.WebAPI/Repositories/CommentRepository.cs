using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repositories.Interfaces;
using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly NewsDbContext _context;

    public CommentRepository(NewsDbContext context)
    {
        _context = context;
    }

    public Comment Add(Comment entity)
    {
        var comment = _context.Comments.Add(entity);
        _context.SaveChanges();

        return comment.Entity;
    }

    public IEnumerable<Comment> GetAll()
    {
        return _context.Comments;
    }

    public Comment Get(int id)
    {
        return _context.Comments.FirstOrDefault(c => c.Id == id);
    }

    public Comment Update(Comment entity)
    {
        var comment = _context.Comments.Update(entity);
        _context.SaveChanges();

        return comment.Entity;
    }

    public Comment Delete(int id)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
        _context.Comments.Remove(comment);
        _context.SaveChanges();

        return comment;
    }

    public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
    {
        return _context.Comments.Where(c => c.ArticleId == articleId).ToList();
    }

    public IEnumerable<Comment> GetCommentByUserId(int userId)
    {
        return _context.Comments.Where(c => c.UserId == userId).ToList();
    }
}