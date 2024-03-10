using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reborn.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool isActivated { get; set; } = false;

        public string activationLink { get; set; } = "test";
    }
}
