using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reborn.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_id { get; set; }
        [Required]
        public  int user_Id { get; set; }

        [Required]
        public int user_id { get; set; }
        public User User { get; set; } = null!;

        public int product_Id { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public string status { get; set; }

        [Required]
        public DateOnly creation_date { get; set; }

        [Required]
        public DateOnly pickup_date { get; set; }

        public Product roduct { get; set; }
        public User user { get; set; }
    }
}