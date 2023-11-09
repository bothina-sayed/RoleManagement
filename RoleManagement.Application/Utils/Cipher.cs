using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Utils
{
    internal static class Cipher
    {
        public static byte[] GenerateRandomKey()
        {
            // Implement secure key generation logic here (e.g., use a KMS or HSM).
            byte[] key = new byte[32]; // 256-bit key
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        public static byte[] Encrypt(string plaintext, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                aesAlg.GenerateIV(); // Generate a random IV of the correct size

                byte[] iv = aesAlg.IV;

                using (var encryptor = aesAlg.CreateEncryptor())
                {
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                    byte[] ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                    // Concatenate IV and ciphertext
                    byte[] encryptedData = new byte[iv.Length + ciphertext.Length];
                    Buffer.BlockCopy(iv, 0, encryptedData, 0, iv.Length);
                    Buffer.BlockCopy(ciphertext, 0, encryptedData, iv.Length, ciphertext.Length);

                    return encryptedData;
                }
            }
        }

        public static string Decrypt(byte[] encryptedData, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                byte[] iv = new byte[aesAlg.BlockSize / 8]; // IV size matches the block size
                byte[] ciphertext = new byte[encryptedData.Length - iv.Length];
                Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(encryptedData, iv.Length, ciphertext, 0, ciphertext.Length);

                aesAlg.IV = iv;

                using (var decryptor = aesAlg.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
