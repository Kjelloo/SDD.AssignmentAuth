namespace SSD.Assignment.Authorization.WebAPI.Model;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}