using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reborn.Models
{
    [Table("products")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public string color { get; set; }

        [Required]
        public string size { get; set; }

        public string image { get; set; }

         public ICollection<Order> Orders { get; }
    }
}
