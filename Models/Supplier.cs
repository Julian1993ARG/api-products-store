using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemAdminProducts.Models
{
    public partial class Supplier
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string? Phone { get; set; }
        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        public  List<Products> Products { get; set; }

    }
}
