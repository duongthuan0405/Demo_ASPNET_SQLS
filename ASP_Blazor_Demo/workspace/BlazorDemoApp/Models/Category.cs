using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorDemoApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
