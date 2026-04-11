using System.Security.Cryptography;
using System.Text;

namespace Web_Novel.Helpers
{
    public static class PasswordHelper
    {
        // =========================
        // HASH PASSWORD (ตอน Register)
        // =========================
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        // =========================
        // VERIFY PASSWORD (ตอน Login)
        // =========================
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashOfInput = HashPassword(inputPassword);
            return hashOfInput == storedHash;
        }
    }
}