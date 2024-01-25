using System.ComponentModel.DataAnnotations;

namespace Reborn.Models
{
    public class LogReg
    {
        [Required]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
