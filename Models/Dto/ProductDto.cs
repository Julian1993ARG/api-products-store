using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SistemAdminProducts.Models.Dto
{
    public class ProductDto
    {
        private double _costPrice;

        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string UpcCode { get; set; }
        public double CostPrice { get => Math.Round(_costPrice, 2); set => _costPrice = value; }
        public double Proffit { get; set; }
        public double SalePrice
        {
            get => Math.Round(CostPrice * Proffit, 2);
            set { }
        }
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

    }
}
