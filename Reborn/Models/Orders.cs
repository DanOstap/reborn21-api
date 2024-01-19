using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reborn.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public  int user_Id { get; set; }
        public int product_Id { get; set; }
        public string status { get; set; }
        public DateOnly creation_date { get; set; }
        public DateOnly pickup_date { get; set; }
    }
}
