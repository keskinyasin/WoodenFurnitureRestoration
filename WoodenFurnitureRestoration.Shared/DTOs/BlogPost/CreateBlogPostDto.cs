using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Shared.DTOs.BlogPost;

public class CreateBlogPostDto
{
    [Required(ErrorMessage = "Blog başlığı zorunludur")]
    [StringLength(200, MinimumLength = 3)]
    public string BlogTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Blog içeriği zorunludur")]
    public string BlogContent { get; set; } = string.Empty;

    public DateTime? PublishedDate { get; set; }

    public string? BlogImage { get; set; }

    [StringLength(500)]
    public string? BlogDescription { get; set; }

    [StringLength(100)]
    public string? BlogAuthor { get; set; }

    [Required(ErrorMessage = "Kategori zorunludur")]
    public int CategoryId { get; set; }

    public int? CustomerId { get; set; }
}