using System.Security.Cryptography;
using System.Text;

namespace FIAP_TDD.Helper
{
    public static class CriptografarSenha
    {
        public static string EncriptografarSenha(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with PBKDF2
            int iterations = 10000;
            byte[] hashBytes = new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(20);

            // Combine the salt and hash
            byte[] hashWithSalt = new byte[36];
            Array.Copy(salt, 0, hashWithSalt, 0, 16);
            Array.Copy(hashBytes, 0, hashWithSalt, 16, 20);

            // Convert the combined salt and hash to a string
            string hashedPassword = Convert.ToBase64String(hashWithSalt);

            return hashedPassword;
        }
    }
}
