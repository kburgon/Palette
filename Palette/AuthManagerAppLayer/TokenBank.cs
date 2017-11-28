using System;
using System.Collections.Concurrent;
using System.Linq;

namespace AuthManagerAppLayer
{
    public class TokenBank
    {
        private ConcurrentBag<Guid> Tokens { get; }
        private static object _bankLock = new object();
        private static TokenBank _bankInstance;

        private TokenBank()
        {
            Tokens = new ConcurrentBag<Guid>();
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

        public bool TokenExists(Guid token) => Tokens.Any(t => t == token);

        public void AddToken(Guid token) => Tokens.Add(token);
    }
}
