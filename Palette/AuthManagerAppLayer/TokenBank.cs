using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManagerAppLayer
{
    public class TokenBank
    {
        private List<Guid> Tokens { get; set; }

        public TokenBank()
        {
            Tokens = new List<Guid> { };
        }

        public bool TokenExists(Guid token) => Tokens.Any(t => t == token);

        public void AddToken(Guid token) => Tokens.Add(token);
    }
}
