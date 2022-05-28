using System;
using System.Collections.Generic;
using System.Text;

namespace BSKStreamCiphers.Models
{
    public class CiphertextAutokey
    {
        private PseudoNumberGenerator png;
        public CiphertextAutokey()
        {
            png = new PseudoNumberGenerator();
        }
        public string Cipher(string seed, string polynomial, string word)
        {
            string result = png.GenerateAutokey(seed, polynomial, word);
            return result;
        }
        public string Decipher(string seed, string polynomial, string cipher)
        {
            string result = png.GenerateAutokeyDecipher(seed, polynomial, cipher);
            return result;
        }
    }
}
