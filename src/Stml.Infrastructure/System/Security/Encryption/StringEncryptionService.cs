﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Stml.Infrastructure.System.Security.Encryption
{
    public class StringEncryptionService : IStringEncryptionService
    {
        protected StringEncryptionOptions Options { get; }

        public StringEncryptionService(IOptions<StringEncryptionOptions> options)
        {
            Options = options.Value;
        }

        public virtual string Encrypt(string plainText, string passPhrase = null, byte[] salt = null)
        {
            if (plainText == null)
            {
                return null;
            }

            if (passPhrase == null)
            {
                passPhrase = Options.DefaultPassPhrase;
            }

            if (salt == null)
            {
                salt = Options.DefaultSalt;
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, salt))
            {
                var keyBytes = password.GetBytes(Options.Keysize / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, Options.InitVectorBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public virtual string Decrypt(string cipherText, string passPhrase = null, byte[] salt = null)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return null;
            }

            if (passPhrase == null)
            {
                passPhrase = Options.DefaultPassPhrase;
            }

            if (salt == null)
            {
                salt = Options.DefaultSalt;
            }

            var cipherTextBytes = Convert.FromBase64String(cipherText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, salt))
            {
                var keyBytes = password.GetBytes(Options.Keysize / 8);
                using (var symmetricKey = Aes.Create())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, Options.InitVectorBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}
