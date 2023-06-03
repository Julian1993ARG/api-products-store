using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class CategoryCreateDto
    {
        [Required]
        [MinLength(3), MaxLength(150)]
        public string Name { get; set; }
        public string Description { get; set; } = "";
    }
}
