using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace CommunicationSubsystem.Security
{
    public class KeyBank
    {
        private List<string> Keys { get; set; }
        private RSACryptoServiceProvider RsaServiceProvider { get; set; }

        public KeyBank()
        {
            Keys = new List<string>();
            RsaServiceProvider = new RSACryptoServiceProvider();
        }

        public string GeneratePublicKey()
        {
            var bankKeys = RsaServiceProvider.ToXmlString(false);
            Keys.Add(bankKeys);
            return RsaServiceProvider.ToXmlString(false);
        }

        public bool IsKeyInBank(string checkKey)
        {
            return Keys.Any(key => key == checkKey);
        }

        /// <summary>
        /// Encrypts the given message using the given public key.
        /// </summary>
        /// <param name="key"> The public key used for encrypting the message. </param>
        /// <param name="encodedBytes"> The message to encrypt. </param>
        /// <returns> The message after being encrypted. </returns>
        /// <exception cref="CryptographicException"> Throws when an invalid key is used. </exception>
        /// <exception cref="System.ArgumentNullException"> Throws when the given message is null. </exception>
        public byte[] Encrypt(string key, byte[] encodedBytes)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);
            return rsa.Encrypt(encodedBytes, false);
        }

        /// <summary>
        /// Decrypts the given message using the RSAServiceProvider's private key.
        /// </summary>
        /// <param name="encryptedBytes"> The message to decrypt. </param>
        /// <returns> The decrypted message. </returns>
        /// <exception cref="CryptographicException"> Throws if the given message wasn't encrypted successfully or there was no private key generated. </exception>
        /// <exception cref="System.ArgumentNullException"> Throws if the given message is null. </exception>
        public byte[] Decrypt(byte[] encryptedBytes)
        {
            return RsaServiceProvider.Decrypt(encryptedBytes, false);
        }
    }
}
