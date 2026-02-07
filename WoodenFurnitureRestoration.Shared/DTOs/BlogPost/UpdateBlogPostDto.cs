namespace WoodenFurnitureRestoration.Shared.DTOs.BlogPost;

public class UpdateBlogPostDto
{
    public string? BlogTitle { get; set; }
    public string? BlogContent { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? BlogImage { get; set; }
    public string? BlogDescription { get; set; }
    public string? BlogAuthor { get; set; }
    public int? CategoryId { get; set; }
}