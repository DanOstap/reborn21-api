using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reborn.Models
{
    public class Cart
    {
        [Required]
        public string name { get; set; }


        [Required]
        public double price { get; set; }

        [Required]
        public string color { get; set; }

        [Required]
        public string size { get; set; }
        [Required]
        public int quantity { get; set; }
    }
}
