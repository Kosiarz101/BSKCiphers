using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCiphers.Models
{
    public interface ICipher
    {
        public string Encrypt(string word, string key);
        public string Decrypt(string word, string key);
    }
}
