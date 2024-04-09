using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Repository.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Repositories.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
    IEnumerable<Comment> GetCommentsByArticleId(int articleId);
    IEnumerable<Comment> GetCommentByUserId(int userId);
}