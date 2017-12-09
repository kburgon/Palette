using System;
using System.Collections.Concurrent;
using System.Linq;
using CommunicationSubsystem.Security;
using log4net;

namespace AuthManagerAppLayer
{
    public class TokenBank
    {
        public string PublicKey { get; }
        private ConcurrentBag<Guid> Tokens { get; }
        private KeyBank Keys { get; }
        private ILog Logger = LogManager.GetLogger(typeof(TokenBank));
        private static object _bankLock = new object();
        private static TokenBank _bankInstance;

        private TokenBank()
        {
            Tokens = new ConcurrentBag<Guid>();
            Keys = new KeyBank();
            PublicKey = Keys.GeneratePublicKey();
            _bankInstance = this;
        }

        public static TokenBank GetInstance()
        {
            lock (_bankLock)
            {
                if (_bankInstance == null)
                {
                    _bankInstance = new TokenBank();
                }

                return _bankInstance;
            }
        }

        public bool EncryptedTokenExists(byte[] encryptedToken)
        {
            try
            {
                var tokenBytes = Keys.Decrypt(encryptedToken);
                return TokenExists(new Guid(tokenBytes));
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public bool TokenExists(Guid token) => Tokens.Any(t => t == token);

        public void AddToken(Guid token) => Tokens.Add(token);
    }
}
