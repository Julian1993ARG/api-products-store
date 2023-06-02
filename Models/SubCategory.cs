using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models
{
    public class SubCategory
    {
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

        public Category? Category { get; set; }
        public IEnumerable<Products>? Products { get; set; }
    }
}
