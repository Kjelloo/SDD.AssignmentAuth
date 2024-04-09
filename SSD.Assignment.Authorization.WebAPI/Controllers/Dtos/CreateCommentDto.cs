namespace SSD.Assignment.Authorization.WebAPI.Controllers.Dtos;

public class CreateCommentDto
{
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
}