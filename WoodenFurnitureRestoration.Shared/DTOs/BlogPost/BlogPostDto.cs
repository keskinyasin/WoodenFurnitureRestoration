namespace WoodenFurnitureRestoration.Shared.DTOs.BlogPost;

public class BlogPostDto
{
    public int Id { get; set; }
    public string BlogTitle { get; set; } = string.Empty;
    public string BlogContent { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string? BlogImage { get; set; }
    public string? BlogDescription { get; set; }
    public string? BlogAuthor { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public DateTime CreatedAt { get; set; }
}