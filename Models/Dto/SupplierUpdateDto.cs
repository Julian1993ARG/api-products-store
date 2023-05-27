﻿using System.ComponentModel.DataAnnotations;

namespace SistemAdminProducts.Models.Dto
{
    public class SupplierUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
