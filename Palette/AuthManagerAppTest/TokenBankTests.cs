using System;
using AuthManagerAppLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem.Security;

namespace AuthManagerAppTest
{
    [TestClass]
    public class TokenBankTests
    {
        [TestMethod]
        public void CanAddTokens()
        {
            var bank = TokenBank.GetInstance();
            var testToken = Guid.NewGuid();

            for (int tokenNumber = 0; tokenNumber < 5; tokenNumber++)
            {
                bank.AddToken(Guid.NewGuid());
            }
            Assert.IsFalse(bank.TokenExists(testToken));
            bank.AddToken(testToken);
            Assert.IsTrue(bank.TokenExists(testToken));
        }

        [TestMethod]
        public void CanCheckEmptyBank()
        {
            var bank = TokenBank.GetInstance();
            Assert.IsFalse(bank.TokenExists(Guid.NewGuid()));
        }

        [TestMethod]
        public void HasSingletonProperties()
        {
            var bank1 = TokenBank.GetInstance();
            var bank2 = TokenBank.GetInstance();
            Assert.AreEqual(bank1, bank2);

            var testToken = Guid.NewGuid();
            bank1.AddToken(testToken);
            Assert.IsTrue(bank2.TokenExists(testToken));
        }

        [TestMethod]
        public void CanFindEncryptedToken()
        {
            var bank = new KeyBank();
            var tokenBank = TokenBank.GetInstance();
            var key = tokenBank.PublicKey;

            var token = Guid.NewGuid();
            tokenBank.AddToken(token);

            var encryptedToken = bank.Encrypt(key, token.ToByteArray());
            Assert.IsTrue(tokenBank.EncryptedTokenExists(encryptedToken));
        }

        [TestMethod]
        public void HandlesBadEncryptedToken()
        {
            var bank = new KeyBank();
            var tokenBank = TokenBank.GetInstance();
            var key = tokenBank.PublicKey;

            var token = Guid.NewGuid();
            var wrongToken = Guid.NewGuid();
            tokenBank.AddToken(token);
            var encryptedToken = bank.Encrypt(key, token.ToByteArray());
            var wrongEncryptedToken = bank.Encrypt(key, wrongToken.ToByteArray());

            Assert.IsFalse(tokenBank.EncryptedTokenExists(wrongEncryptedToken));

            Assert.IsTrue(tokenBank.EncryptedTokenExists(encryptedToken));
            encryptedToken[0] += 5;
            Assert.IsFalse(tokenBank.EncryptedTokenExists(encryptedToken));
        }
    }
}
