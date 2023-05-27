using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string Decription { get; set; }
        [Required]
        [MaxLength(20)]
        public string UpcCode { get; set; }
        public double Price { get; set; }
        public int? SupplierId { get; set; }

    }
}
