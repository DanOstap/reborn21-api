using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reborn.Models
{
    [Table("products")]
    public class Product
    {
<<<<<<< Updated upstream
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
=======
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int product_id { get; set; }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
        public ICollection<Order> Order { get; set; }
>>>>>>> Stashed changes
    }
}
