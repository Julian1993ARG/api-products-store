using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemAdminProducts.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = "";
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
