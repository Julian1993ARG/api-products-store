using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
