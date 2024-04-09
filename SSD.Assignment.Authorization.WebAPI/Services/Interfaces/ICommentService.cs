using SSD.Assignment.Authorization.WebAPI.Model;

namespace SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

public interface ICommentService : IService<Comment>
{
    IEnumerable<Comment> GetCommentsByArticleId(int articleId);
    IEnumerable<Comment> GetCommentsByUserId(int userId);
}