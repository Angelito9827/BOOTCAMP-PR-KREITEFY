using bootcamp_pr_kreitefy_api.Application.Services;
using System.Security.Cryptography;

namespace bootcamp_pr_kreitefy_api.Infrastructure.ExternalServices
{
    public class PasswordHasher : IPasswordHasher
    {

        public const int SaltSize = 32;
        public const int HashSize = 32;
        public const int Iterations = 150000;
        public const char Separator = '-';
        public static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                Algorithm,
                HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            string[] parts = hashedPassword.Split(Separator);
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                Algorithm,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
