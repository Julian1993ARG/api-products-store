namespace SistemAdminProducts.Models.Dto
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
        public CategoryUpdateDto? Category { get; set; }
        public virtual ICollection<Products>? Products { get; set; }
    }
}
