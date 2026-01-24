using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WoodenFurnitureRestoration.Entities
{
    public class BlogPost : IEntity
    {
        public BlogPost() { }

        // ✅ IEntity Properties
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // ✅ BlogPost Properties
        [Required(ErrorMessage = "Lütfen blog başlığını belirtiniz.")]
        [StringLength(255, ErrorMessage = "Blog başlığı 255 karakterden uzun olamaz.")]
        public string BlogTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen blog içeriğini belirtiniz.")]
        public string BlogContent { get; set; } = string.Empty;

        [Required]
        public DateTime PublishedDate { get; set; }

        [DataType(DataType.ImageUrl, ErrorMessage = "Geçersiz görsel URL'si.")]
        public string? BlogImage { get; set; }

        [StringLength(500, ErrorMessage = "Blog açıklaması 500 karakterden uzun olamaz.")]
        public string? BlogDescription { get; set; }

        [StringLength(100, ErrorMessage = "Blog yazarı 100 karakterden uzun olamaz.")]
        public string? BlogAuthor { get; set; }

        // ✅ Foreign Keys (SADECE GEREK OLANLAR)
        [Required]
        public int CategoryId { get; set; }

        public int? CustomerId { get; set; }

        // ✅ Navigation Properties
        public virtual Category Category { get; set; } = null!;
        public virtual Customer? Customer { get; set; }
        public virtual Address? Address { get; set; }           
        public virtual Shipping? Shipping { get; set; }       
        public virtual Restoration? Restoration { get; set; }   
        public virtual Review? Review { get; set; }

        // ✅ Collections
        public virtual ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();

        // Constructor
        public BlogPost(
            string blogTitle,
            string blogContent,
            int categoryId,
            DateTime? publishedDate = null,
            int? customerId = null,
            string? blogImage = null,
            string? blogDescription = null,
            string? blogAuthor = null)
        {
            BlogTitle = blogTitle ?? throw new ArgumentNullException(nameof(blogTitle));
            BlogContent = blogContent ?? throw new ArgumentNullException(nameof(blogContent));
            CategoryId = categoryId;
            PublishedDate = publishedDate ?? DateTime.Now;
            CustomerId = customerId;
            BlogImage = blogImage;
            BlogDescription = blogDescription;
            BlogAuthor = blogAuthor;
        }
    }
}