using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemAdminProducts.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(20)]
        public string UpcCode { get; set; }
        [DefaultValue(0.0)]
        public double CostPrice { get; set; }
        [DefaultValue(1.5)]
        public double Proffit { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}
