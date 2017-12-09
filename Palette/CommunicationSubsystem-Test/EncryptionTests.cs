using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CommunicationSubsystem.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void CanEncryptDecryptMessage()
        {
            var bank = new KeyBank();
            var message = new byte[] {5, 3, 2};
            var key = bank.GeneratePublicKey();

            // verfy that the message is different after encryption.
            var encryptedMessage = bank.Encrypt(key, message);
            Assert.AreNotEqual(message.Length, encryptedMessage.Length);

            // Verify that the decrypted message is the same as the message before encryption.
            var decryptedMessage = bank.Decrypt(encryptedMessage);
            Assert.AreEqual(message.Length, decryptedMessage.Length);

            for (int byteIndex = 0; byteIndex < message.Length; byteIndex++)
            {
                Assert.AreEqual(message[byteIndex], decryptedMessage[byteIndex]);
            }
        }

        [TestMethod]
        public void CanEncryptDecryptGuid()
        {
            var bank = new KeyBank();
            var guidMessage = Guid.NewGuid();
            var message = guidMessage.ToByteArray();
            var key = bank.GeneratePublicKey();

            // Verify that the message is different after encryption.
            var encryptedMessage = bank.Encrypt(key, message);
            Assert.AreNotEqual(message.Length, encryptedMessage.Length);

            // Verify that the message has been decrypted successfully.
            var decryptedMessage = bank.Decrypt(encryptedMessage);
            Assert.AreEqual(message.Length, decryptedMessage.Length);

            for (int byteIndex = 0; byteIndex < message.Length; byteIndex++)
            {
                Assert.AreEqual(message[byteIndex], decryptedMessage[byteIndex]);
            }

            // Verify that the decrypted message can be converted back into the same guid.
            var decryptedGuid = new Guid(decryptedMessage);
            Assert.AreEqual(guidMessage, decryptedGuid);
        }

        [TestMethod]
        public void ThrowsOnFailToEncryptUnencryptedMessage()
        {
            var bank = new KeyBank();
            var key = bank.GeneratePublicKey();
            var message = Guid.NewGuid().ToByteArray();
            Assert.ThrowsException<CryptographicException>(() => bank.Decrypt(message));
        }

        [TestMethod]
        public void ThrowsOnFailToEncryptTamperedMessage()
        {
            var bank = new KeyBank();
            var key = bank.GeneratePublicKey();
            var message = Guid.NewGuid().ToByteArray();

            var encryptedMessage = bank.Encrypt(key, message);
            var tamperedMessageValues = encryptedMessage.Select(value => Convert.ToByte(value > 50 ? value - 10 : value + 5));

            Assert.ThrowsException<CryptographicException>(() => bank.Decrypt(tamperedMessageValues.ToArray()));
            Assert.AreEqual(message.Length, bank.Decrypt(encryptedMessage).Length);
        }

        [TestMethod]
        public void ThrowsOnReceiveNullMessage()
        {
            var bank = new KeyBank();
            var key = bank.GeneratePublicKey();

            Assert.ThrowsException<System.ArgumentNullException>(() => bank.Encrypt(key, null));
            Assert.ThrowsException<System.ArgumentNullException>(() => bank.Decrypt(null));
        }

        [TestMethod]
        public void ThrowsWithBadKey()
        {
            var bank1 = new KeyBank();
            var bank2 = new KeyBank();
            var key1 = bank1.GeneratePublicKey();
            var key2 = bank2.GeneratePublicKey();
            var message = Guid.NewGuid().ToByteArray();

            var encryptedMessage1 = bank1.Encrypt(key1, message);
            var encryptedMessage2 = bank2.Encrypt(key2, message);

            Assert.ThrowsException<CryptographicException>(() => bank2.Decrypt(encryptedMessage1));
            Assert.ThrowsException<CryptographicException>(() => bank1.Decrypt(encryptedMessage2));
        }
    }
}
