using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Products>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Products>? Products { get; set; }
    }
}
