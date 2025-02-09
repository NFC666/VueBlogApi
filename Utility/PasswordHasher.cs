using System.Security.Cryptography;
using System.Text;

namespace VueBlog.API.Utility
{
    public class PasswordHasher
    {

        public static string SaltGenerator()
        {
            var bytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] hashedPasswordBytes;
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var saltedPassword = password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
            }

            return Convert.ToBase64String(hashedPasswordBytes);
        }
        public static string HashPassword(string password)
        {
            byte[] hashedPasswordBytes;
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var saltedPassword = password;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
            }

            return Convert.ToBase64String(hashedPasswordBytes);
        }
    }
}
