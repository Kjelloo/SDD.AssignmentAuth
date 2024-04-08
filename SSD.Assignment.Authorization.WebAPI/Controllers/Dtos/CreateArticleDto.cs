namespace SSD.Assignment.Authorization.WebAPI.Controllers.Dtos;

public class CreateArticleDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}