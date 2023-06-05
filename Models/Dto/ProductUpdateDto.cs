using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(20)]
        public string UpcCode { get; set; }
        public double CostPrice { get; set; } = 0;
        public double Proffit { get; set; } = 1.5;
        public int SupplierId { get; set; }
        public int SubCategoryId { get; set; }

    }
}
