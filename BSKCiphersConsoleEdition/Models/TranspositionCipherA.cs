using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSKCiphersConsoleEdition.Models
{
    public class TranspositionCipherA : ICipher
    {
        public string Decrypt(string word, string key)
        {
            //Usunięcie białych znaków
            word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

            //Usuń znaki inne niż cyfry
            string adaptKey = AdaptKey(key);
            //Jezeli nie da się przekonwertować znaków na liczbę całkowitą, wyrzuć błąd
            if (!Int32.TryParse(adaptKey, out int result))
            {
                throw new ArgumentException("String is not in correct format: d-(...)-d");
            }

            //Potrzebne Dane
            int rowCount = (int)Math.Ceiling((decimal)word.Length / (decimal)adaptKey.Length);
            char[,] array = new char[rowCount, adaptKey.Length];
            string decryptedWord = "";
            int k = 0;
            
            //Dostosuj szyfr
            word = AdaptCipher(word, adaptKey);
            //Przepisz słowo do tablicy w kolejności odwiedzania kolumn zapisanej w kluczu
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, Int32.Parse(adaptKey[j].ToString()) - 1] = word[k];
                    k++;
                    if (k >= word.Length)
                        break;
                }
            }
            //Odczytaj słowo z tablicy
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    decryptedWord += array[i, j];
                }
            }
            return decryptedWord;
        }

        public string Encrypt(string word, string key)
        {
            //Usunięcie białych znaków
            word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

            //Usuń wszystkie znaki oprócz liczb
            string adaptKey = AdaptKey(key);
            //sprawdź czy można zrzutować string w tej postaci na liczbę całkowitą
            if(!Int32.TryParse(adaptKey, out int result))
            {
                throw new ArgumentException("String is not in correct format: cannot cast to integer");
            }

            //Potrzebne Dane
            int rowCount = (int)Math.Ceiling((decimal)word.Length / (decimal)adaptKey.Length);
            char[,] array = new char[rowCount, adaptKey.Length];
            string encryptedWord = "";
            int k = 0;
            
            //Wypełnij słowem tablicę
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] += word[k];
                    k++;
                    if (k >= word.Length)
                        break;
                }
            }

            //Przejdź od najniższego do najwyższego wiersza, odwiedzając kolumny w kolejności podanej przez klucz
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    encryptedWord += array[i, Int32.Parse(adaptKey[j].ToString())-1];
                }
            }
            return encryptedWord;
        }
        private string AdaptKey(string key)
        {
            string resultString = new String(key.Where(Char.IsDigit).ToArray());
            return resultString;
        }
        private string AdaptCipher(string word, string key)
        {
            //Kolejka przechwoująca pozycje do wypełnienia
            Queue<int> positions = new Queue<int>();
            //indeksy kolumn wyższe od tej liczby zostaną zapisane w kolejce
            int charCount = word.Length % key.Length;
            //Zapisz odpowiednie pozycje do kolejki
            for(int i=0; i<key.Length; i++)
            {
                if(Int32.Parse(key[i].ToString()) > charCount)
                {
                    positions.Enqueue(((word.Length - charCount) + i));
                }
            }
            string adaptedCipher = "";
            int n = word.Length + positions.Count;
            int j = 0;
            //Przepisz słowo zaszyfrowane dodając biały znak na pozycjach zapisanych w kolejce
            for(int i=0; i<n; i++)
            {
                if(positions.Contains(i))
                {
                    adaptedCipher += " ";
                    positions.Dequeue();                    
                }
                else
                {
                    adaptedCipher += word[j];
                    j++;
                }               
            }
            return adaptedCipher;
        }
    }
}
