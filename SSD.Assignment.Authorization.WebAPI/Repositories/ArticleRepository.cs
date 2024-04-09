using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.DbContext;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Repositories;

public class ArticleRepository : IRepository<Article>
{
    private readonly NewsDbContext _context;

    public ArticleRepository(NewsDbContext context)
    {
        _context = context;
    }

    public Article Add(Article entity)
    {
        var article = _context.Articles.Add(entity);
        _context.SaveChanges();

        return article.Entity;
    }

    public IEnumerable<Article> GetAll()
    {
        return _context.Articles;
    }

    public Article Get(int id)
    {
        return _context.Articles.FirstOrDefault(a => a.Id == id);
    }

    public Article Update(Article entity)
    {
        var article = _context.Articles.Update(entity);
        _context.SaveChanges();

        return article.Entity;
    }

    public Article Delete(int id)
    {
        var article = _context.Articles.FirstOrDefault(a => a.Id == id);
        _context.Articles.Remove(article);
        _context.SaveChanges();

        return article;
    }
}