﻿namespace SistemAdminProducts.Models.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public IEnumerable<SubCategory>? SubCategories { get; set; }
    }
}
