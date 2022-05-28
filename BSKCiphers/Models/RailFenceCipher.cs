using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKCiphers.Models
{
    public class RailFenceCipher : ICipher
    {
		public string Encrypt(string word, string key)
		{
			//Usunięcie białych znaków
			word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

			//Potrzebne dane
			int rowBiggestIndex = Int32.Parse(key) - 1;
			char[,] array = new char[Int32.Parse(key), word.Length];
			int currentRow = 0;
			int iterator = 1; 
			
			//Wypełnienie tabeli słowem
			for (int i = 0; i < array.GetLength(1); i++)
			{
				array[currentRow, i] = word[i];
				if (currentRow == 0)
				{
					iterator = 1;
				}
				else if (currentRow == rowBiggestIndex)
				{
					iterator = -1;
				}
				currentRow += iterator;
			}

			//Przepisanie słowa z tablicy do zwracanej zmiennej
			string encryptedWord = "";
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					encryptedWord += array[i, j];
				}
			}
			return encryptedWord.Replace("\0", "");
		}
		public string Decrypt(string word, string key)
		{
			word = String.Concat(word.Where(c => !Char.IsWhiteSpace(c)));

			int rowCount = Int32.Parse(key) - 1;
			char[,] array = new char[Int32.Parse(key), word.Length];
			string decryptedWord = "";
			int currentRow = 0;
			int iterator = 1;

			//Wyznacz miejsca do wpisywania liter
			for (int i = 0; i < array.GetLength(1); i++)
			{
				array[currentRow, i] = '*';
				if (currentRow == 0)
				{
					iterator = 1;
				}
				else if (currentRow == rowCount)
				{
					iterator = -1;
				}
				currentRow += iterator;
			}

			//Wypełnienie tablicy słowem - litery są stawiane w miejsce * (gwiazdek)
			int k = 0;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					if(array[i,j] == '*')
                    {
						array[i, j] = word[k];
						k++;
					}						
				}
			}

			//Przechodzenie przez tablicę zygzakiem - odczytywanie zaszyfrowanej wiadomości
			currentRow = 0;
			for (int i = 0; i < array.GetLength(1); i++)
			{
				decryptedWord += array[currentRow, i];
				if (currentRow == 0)
				{
					iterator = 1;
				}
				else if (currentRow == rowCount)
				{
					iterator = -1;
				}
				currentRow += iterator;
			}
			return decryptedWord;
		}
	}
}
