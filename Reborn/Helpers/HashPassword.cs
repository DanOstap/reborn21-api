using System.Security.Cryptography;
using System.Text;

namespace Reborn.Helpers
{
    public class HashPassword
    {
        public string HashPaswword(string password) {
            using (var sha256 = SHA256.Create()) {
                var HashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var Hash = BitConverter.ToString(HashedBytes).Replace("-", "").ToLower();
                return Hash;
            }
            
        }
    }
}
