using System.ComponentModel.DataAnnotations;

namespace ParamAPI.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Range(5,1000)]
        public int Stock { get; set; }

        [Range(10,10000)]
        public double Price { get; set; }
    }
}
