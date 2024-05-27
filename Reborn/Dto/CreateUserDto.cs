using System.ComponentModel.DataAnnotations;

namespace Reborn.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
