using BSKCiphersConsoleEdition.Models;
using System;
using System.Collections.Generic;

namespace BSKCiphersConsoleEdition
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKey keyEntered;
                int optionSelected = 0;
                do
                {
                    Console.Clear();
                    string[] options = { "Rail Fence Cipher", "Transposition Cipher A", "Transposition Cipher B" };
                    Console.WriteLine("BSK Ciphers vol. 1");
                    Console.WriteLine("");
                    Console.WriteLine("Choose option");
                    showOptions(options, optionSelected);

                    keyEntered = ReadKey();
                    optionSelected = CheckArrowKey(keyEntered, optionSelected, options);

                } while (keyEntered != ConsoleKey.Enter && keyEntered != ConsoleKey.Escape);

                if (keyEntered == ConsoleKey.Escape)
                    Environment.Exit(0);

                switch (optionSelected)
                {
                    case 0:
                        ExecuteCipher(new RailFenceCipher());
                        break;
                    case 1:
                        ExecuteCipher(new TranspositionCipherA());
                        break;
                    case 2:
                        ExecuteCipher(new TranspositionCipherB());
                        break;
                    default:
                        ExecuteCipher(new RailFenceCipher());
                        break;
                }
            }
        }

        private static void ExecuteCipher(ICipher cipher)
        {
            ConsoleKey keyEntered;
            int optionSelected = 0;

            do
            {
                Console.Clear();
                string[] options = { "Encrypt", "Decrypt" };
                showOptions(options, optionSelected);

                keyEntered = ReadKey();
                optionSelected = CheckArrowKey(keyEntered, optionSelected, options);

            } while (keyEntered != ConsoleKey.Enter && keyEntered != ConsoleKey.Escape);

            if (keyEntered == ConsoleKey.Escape)
                return;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter Word");
                string word = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("Enter Key");
                string key = Console.ReadLine();

                string result;

                try
                {
                    if (optionSelected == 0)
                        result = cipher.Encrypt(word, key);
                    else
                        result = cipher.Decrypt(word, key);
                }
                catch(ArgumentException e)
                {
                    continue;
                }

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Result: " + result);
                Console.ForegroundColor = ConsoleColor.White;
                keyEntered = ReadKey();

            } while (keyEntered != ConsoleKey.Escape);

            return;
        }

        private static void showOptions(string[] options, int optionSelected)
        {
            Console.WriteLine("");
            for (int i = 0; i < options.Length; i++)
            {
                if (optionSelected == i)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{options[i]}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static int CheckArrowKey(ConsoleKey keyEntered, int selectedOption, IList<string> options)
        {
            if (keyEntered == ConsoleKey.UpArrow)
            {
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = options.Count - 1;
                return selectedOption;
            }
            else if (keyEntered == ConsoleKey.DownArrow)
            {
                selectedOption = (selectedOption + 1) % options.Count;
                return selectedOption;
            }
            return selectedOption;
        }

        public static ConsoleKey ReadKey()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            return consoleKeyInfo.Key;
        }
    }
}
