using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reborn.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string status { get; set; }

        [Required]
        public DateOnly creation_date { get; set; }

        [Required]
        public DateOnly pickup_date { get; set; }
        
        [Required]
        public Product Product { get; set; }
        public int Product_Id{get;set;}
        
        [Required]
        public User User { get; set; }
        public int User_Id{get;set;}
    }
}
