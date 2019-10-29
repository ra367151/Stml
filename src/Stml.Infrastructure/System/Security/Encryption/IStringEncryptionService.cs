using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.System.Security.Encryption
{
    public interface IStringEncryptionService
    {
        string Encrypt(string plainText, string passPhrase = null, byte[] salt = null);
        string Decrypt(string cipherText, string passPhrase = null, byte[] salt = null);
    }
}
