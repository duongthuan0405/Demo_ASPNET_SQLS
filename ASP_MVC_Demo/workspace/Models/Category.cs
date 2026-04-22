using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace workspace.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
