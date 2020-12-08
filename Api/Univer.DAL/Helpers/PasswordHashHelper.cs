using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Univer.DAL.Helpers
{
    public static class PasswordHashHelper
    {
        private const int Iterations = 10000;
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public static string HashPassword(this string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Iterations,
              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Iterations}.{salt}.{key}";
            }
        }

        public static (bool Verified, bool NeedsUpgrade) CheckPasswordWithHash(this string password, string hash)
        {
            var parts = hash.Split('.', 3);

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }
    }
}
