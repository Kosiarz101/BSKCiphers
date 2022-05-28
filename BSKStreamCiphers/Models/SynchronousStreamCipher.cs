using System;
using System.Collections.Generic;
using System.Text;

namespace BSKStreamCiphers.Models
{
    public class SynchronousStreamCipher
    {
        public string Cipher(string key, string word)
        {
            string result = "";
            for(int i=0; i<word.Length; i++)
            {
                char xorResult = PseudoNumberGenerator.XOROperation(key[i], word[i]);
                result += xorResult;
            }
            return result;
        }
        public string Cipher(string word, string seed, string polynomial)
        {
            PseudoNumberGenerator pseudoNumberGenerator = new PseudoNumberGenerator();
            List<string> generatorResult = pseudoNumberGenerator.GenerateList(seed, polynomial, word.Length);
            List<int> positions = PseudoNumberGenerator.CalculatePositions(polynomial);
            string result = "";
            for (int i = 0; i < word.Length; i++)
            {
                //operacja xor na itym obiekcie wynikowej listy generatora
                char xorResultKey = PseudoNumberGenerator.XOROperation(positions, generatorResult[i]);
                //operacja xor na wyniku poprzedniej operacji oraz itej literze słowa
                char xorResultKeyAndWord = PseudoNumberGenerator.XOROperation(xorResultKey, word[i]);
                //dodaj do szyfru
                result += xorResultKeyAndWord;
            }
            return result;
        }
    }
}
