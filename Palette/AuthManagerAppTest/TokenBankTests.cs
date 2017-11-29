using System;
using AuthManagerAppLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
