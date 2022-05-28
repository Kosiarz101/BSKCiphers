using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCiphers.Models
{
    public class TranspositionCipherB : ICipher
    {
        public string Decrypt(string word, string key)
        {
            word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

            int rowCount = (int)Math.Ceiling((decimal)word.Length / (decimal)key.Length);
            char[,] array = new char[rowCount, key.Length];
            string decryptedWord = "";
            int k = 0;
            
            char[] keyArray = key.ToArray();
            Dictionary<char, Queue<int>> keyDictionary = new Dictionary<char, Queue<int>>();
            for (int i = 0; i < keyArray.Length; i++)
            {
                if (!keyDictionary.ContainsKey(keyArray[i]))
                {
                    Queue<int> temp = new Queue<int>();
                    temp.Enqueue(i);
                    keyDictionary.Add(keyArray[i], temp);
                }
                else
                {
                    keyDictionary[keyArray[i]].Enqueue(i);
                }
            }

            Array.Sort(keyArray);
            //kolumny o większym lub równym indeksie mają ostatni wiersz pusty
            int charCount = word.Length % key.Length;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                //Wybierz kolumnę do wypełnienia
                int currentColumn = keyDictionary[keyArray[j]].Dequeue();
                int decrementer = 0;
                if (currentColumn >= charCount)
                    decrementer = 1;
                //Wypełnij wszystkie wiersze kolumny
                for (int i = 0; i < array.GetLength(0) - decrementer; i++)
                {
                    array[i, currentColumn] += word[k];
                    k++;
                }
            }

            //Przepisz słowo z tablicy do zwracanej zmiennej
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    decryptedWord += array[i, j];
                }
            }
            return decryptedWord.Replace("\0", "");
        }

        public string Encrypt(string word, string key)
        {
            //Usuwanie białych znaków
            word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

            //Potrzebne Dane związane z tablicą
            int rowCount = (int)Math.Ceiling((decimal)word.Length / (decimal)key.Length);
            char[,] array = new char[rowCount, key.Length];
            string encryptedWord = "";
            int k = 0;            

            /* Słownik o kluczu będącym literą w kluczu podanym przez użytkownika. Pod kluczem słownika kryje się kolejka typu int - 
             * zapisane są tam indeksy występowania litery (klucza słownika) w kluczu podanym przez użytkownika (klucza szyfru)*/ 
            char[] keyArray = key.ToArray();
            Dictionary<char, Queue<int>> keyDictionary = new Dictionary<char, Queue<int>>();

            //Odpowiednie wypełnienie słownika
            for(int i=0; i<keyArray.Length; i++)
            {
                //Jeżeli jest to pierwsze wystapienie litery
                if(!keyDictionary.ContainsKey(keyArray[i]))
                {
                    Queue<int> temp = new Queue<int>();
                    temp.Enqueue(i);
                    keyDictionary.Add(keyArray[i], temp);
                }
                //Jeżeli jest to kolejne wystąpienie tej samej litery
                else
                {
                    keyDictionary[keyArray[i]].Enqueue(i);
                }
                
            }

            Array.Sort(keyArray);
            //Wypełnienie tablicy słowem
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

            /*Zapisanie w zmiennej słowa z tablicy, przechodząc od "góry do dołu", wybierając kolumny zgodnie
             z kolejnością klucza podanego przez użytkownika*/            
            for (int j = 0; j < array.GetLength(1); j++)
            {
                //Wyjmij z odpowiedniej kolejki pozycję litery w kluczu szyfru
                int currentColumn = keyDictionary[keyArray[j]].Dequeue();
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    encryptedWord += array[i, currentColumn];
                }
            }
            return encryptedWord.Replace("\0", "");  
        }
    }
}
