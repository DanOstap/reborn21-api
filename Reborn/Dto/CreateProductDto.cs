using System.ComponentModel.DataAnnotations;

namespace Reborn.Dto
{
    public class CreateProductDto
    {
        [Required]
        public string name { get; set; }
        
        [Required]
        public string type { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public string color { get; set; }

        [Required]
        public string size { get; set; }
        
        public string image { get; set; }

    }
}
