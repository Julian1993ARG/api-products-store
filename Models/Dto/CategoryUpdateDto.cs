using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(150)]
        public string Name { get; set; }
        public string Description { get; set; } = "";
    }
}
