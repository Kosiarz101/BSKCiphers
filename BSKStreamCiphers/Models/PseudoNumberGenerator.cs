using System;
using System.Collections.Generic;
using System.Text;

namespace BSKStreamCiphers.Models
{
    public class PseudoNumberGenerator
    {
        public string Generate(string seed, string polynomial, int Length)
        {
            List<int> positions = new List<int>();
            //Znajdowanie pozycji na których powinna być wykonwyana operacja XOR
            for(int i=0; i<polynomial.Length; i++)
            {
                if(polynomial[i] == '1')
                    positions.Add(i);
            }
            List<char> result = new List<char>();
            List<char> finalResult = new List<char>();
            string currentSeed = seed;
            for(int i=0; i<Length; i++)
            {
                finalResult.Add(XOROperation(positions, currentSeed));
                //Wykonaj operację XOR
                char xorResult = XOROperation(positions, currentSeed);
                //Dodaj Najmłodszy bit do rejestru
                result.Add(currentSeed[currentSeed.Length - 1]);
                //Przesuń rejestr w prawo o 1 bit oraz wstaw wynik operacji XOR na pozycję najstarszego bitu
                currentSeed = ShiftRegister(xorResult, currentSeed);               
            }
            return new string(finalResult.ToArray());
        }
        public List<string> GenerateList(string seed, string polynomial, int length)
        {
            //Znajdowanie pozycji na których powinna być wykonwyana operacja XOR
            List<int> positions = CalculatePositions(polynomial);
            List<string> finalResult = new List<string>();
            string currentSeed = seed;
            for (int i = 0; i < length; i++)
            {
                //Dodaj do wynikowej listy
                finalResult.Add(currentSeed);
                //Wykonaj operację XOR
                char xorResult = XOROperation(positions, currentSeed);
                //Przesuń rejestr w prawo o 1 bit oraz wstaw wynik operacji XOR na pozycję najstarszego bitu
                currentSeed = ShiftRegister(xorResult, currentSeed);
            }
            return finalResult;
        }
        public void GenerateEndless(string seed, string polynomial)
        {
            List<int> positions = new List<int>();
            //Znajdowanie pozycji na których powinna być wykonwyana operacja XOR
            for (int i = 0; i < polynomial.Length; i++)
            {
                if (polynomial[i] == '1')
                    positions.Add(i);
            }
            List<char> result = new List<char>();
            string currentSeed = seed;
            while(true)
            {
                Console.WriteLine(currentSeed);
                System.Threading.Thread.Sleep(500);
                //Wykonaj operację XOR
                char xorResult = XOROperation(positions, currentSeed);
                //Dodaj Najmłodszy bit do rejestru
                result.Add(currentSeed[currentSeed.Length - 1]);
                //Przesuń rejestr w prawo o 1 bit oraz wstaw wynik operacji XOR na pozycję najstarszego bitu
                currentSeed = ShiftRegister(xorResult, currentSeed);               
            }
        }

        public string GenerateAutokey(string seed, string polynomial, string word)
        {
            List<int> positions = new List<int>();
            int Length = word.Length;
            //Znajdowanie pozycji na których powinna być wykonwyana operacja XOR
            for (int i = 0; i < polynomial.Length; i++)
            {
                if (polynomial[i] == '1')
                    positions.Add(i);
            }
            //Wynik szyfrowania
            List<char> result = new List<char>();
            string currentSeed = seed;
            for (int i = 0; i < Length; i++)
            {
                //Wykonaj operację XOR dla bitów ziarna wskazanych przez wielomian
                char xorSeedResult = XOROperation(positions, currentSeed);
                //Wykonaj operację XOR z wynikiem poprzedniego XOR i i-tym bitem słowa
                char xorWithWordResult = XOROperation(word[i], xorSeedResult);
                //Dodaj wynik operacji do listy
                result.Add(xorWithWordResult);
                //Przesuń rejestr w prawo o 1 bit oraz wstaw wynik operacji XOR na pozycję najstarszego bitu
                currentSeed = ShiftRegister(xorWithWordResult, currentSeed);
            }
            return new string(result.ToArray());
        }
        public string GenerateAutokeyDecipher(string seed, string polynomial, string cipher)
        {
            List<int> positions = new List<int>();
            int Length = cipher.Length;
            //Znajdowanie pozycji na których powinna być wykonwyana operacja XOR
            for (int i = 0; i < polynomial.Length; i++)
            {
                if (polynomial[i] == '1')
                    positions.Add(i);
            }
            //Wynik szyfrowania
            List<char> result = new List<char>();
            string currentSeed = seed;
            for (int i = 0; i < Length; i++)
            {
                //Wykonaj operację XOR dla bitów ziarna wskazanych przez wielomian
                char xorSeedResult = XOROperation(positions, currentSeed);
                //Wykonaj operację XOR z wynikiem poprzedniego XOR i i-tym bitem słowa
                char xorWithWordResult = XOROperation(cipher[i], xorSeedResult);
                //Dodaj wynik operacji do listy
                result.Add(xorWithWordResult);
                //Przesuń rejestr w prawo o 1 bit
                currentSeed = ShiftRegister(cipher[i], currentSeed);
            }
            return new string(result.ToArray());
        }

        private string ShiftRegister(char xorResult, string seed)
        {
            //Dodaj wynik operacji XOR na pierwszą pozycję
            string newSeed = xorResult.ToString();
            //Dodaj resztę ciągu na dalszą pozycję
            newSeed += seed;
            //Usuń ostatni bit z nowego ciągu
            newSeed = newSeed.Remove(newSeed.Length - 1);
            return newSeed;
        }
        public static List<int> CalculatePositions(string polynomial)
        {
            List<int> positions = new List<int>();
            //Przejdź po każdej cyfrze wielomianu
            for (int i = 0; i < polynomial.Length; i++)
            {
                //jeżeli cyfra wynosi jeden, dodaj jej index do listy pozycji
                if (polynomial[i] == '1')
                    positions.Add(i);
            }
            return positions;
        }
        public static char XOROperation(List<int> positions, string seed)
        {
            //przechwouje liczbę jedynek w ziarnie (seed)
            int oneCount = 0;
            for(int i=0; i<positions.Count; i++)
            {
                //jeżeli cyfra na pozycji ziarna wyliczonej przez funkcję CalculatePositions() wynosi 1, dodaj do licznika jedynek
                if (seed[positions[i]] == '1')
                    oneCount++;
            }
            //Jeżeli liczba jedynek jest nieparzysta, zwróć 1 jako wyniki operacji XOR, w przeciwnym przypadku zwróć zero
            return oneCount % 2 == 1 ? '1' : '0'; 
        }
        public static char XOROperation(char bit1, char bit2)
        {
            if (bit1 == bit2)
                return '0';
            else
                return '1';
        }
    }
}
