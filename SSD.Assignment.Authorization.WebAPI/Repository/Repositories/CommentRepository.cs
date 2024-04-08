using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Repository.Repositories;

public class CommentRepository : IRepository<Comment>
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
}