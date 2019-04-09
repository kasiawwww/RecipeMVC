using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models.DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
