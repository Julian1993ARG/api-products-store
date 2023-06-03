using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class SubCategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [Required]
        public int CategoryId { get; set; }
    }
}
