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
        public int UpcCode { get; set; }
    }
}
