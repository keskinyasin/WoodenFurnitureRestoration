using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Entities
{
    public class BlogPost : IEntity
    {
        public BlogPost() { } // Parametresiz yapıcı metot

        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen blog başlığını belirtiniz.")]
        [StringLength(255, ErrorMessage = "Blog başlığı 255 karakterden uzun olamaz.")]
        public string BlogTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen blog içeriğini belirtiniz.")]
        public string BlogContent { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [DataType(DataType.ImageUrl, ErrorMessage = "Geçersiz görsel URL'si.")]
        public string? BlogImage { get; set; }

        [StringLength(500, ErrorMessage = "Blog açıklaması 500 karakterden uzun olamaz.")]
        public string? BlogDescription { get; set; }

        [StringLength(100, ErrorMessage = "Blog yazarı 100 karakterden uzun olamaz.")]
        public string? BlogAuthor { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [Required]
        public int ShippingId { get; set; }
        public virtual Shipping Shipping { get; set; } = null!;

        [Required]
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; } = null!;

        [Required]
        public int RestorationId { get; set; }
        public virtual Restoration Restoration { get; set; } = null!;

        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();

        // IEntity properties
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        // Constructor with null checks
        public BlogPost(string blogTitle, string blogContent, int customerId, int categoryId, int shippingId, int reviewId, int restorationId, int addressId)
        {
            BlogTitle = blogTitle ?? throw new ArgumentNullException(nameof(blogTitle));
            BlogContent = blogContent ?? throw new ArgumentNullException(nameof(blogContent));
            CustomerId = customerId;
            CategoryId = categoryId;
            ShippingId = shippingId;
            ReviewId = reviewId;
            RestorationId = restorationId;
            AddressId = addressId;
        }
    }
}

