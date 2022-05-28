using BSKStreamCiphers.Models;
using System;
using System.Collections.Generic;

namespace BSKStreamCiphers
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                ConsoleKey keyEntered;
                int optionSelected = 0;
                do
                {
                    Console.Clear();
                    string[] options = { "Number Generator", "Synchronous Stream Cipher", "Ciphertext Autokey" };
                    Console.WriteLine("BSK Ciphers vol. 2");
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
                        NumberGenerator();
                        break;
                    case 1:
                        SynchronousStreamCipherExecute();
                        break;
                    case 2:
                        CiphertextAutokey();
                        break;
                    default:
                        NumberGenerator();
                        break;
                }
            }
            
            

            //string word = "11101001";
            //PseudoNumberGenerator pseudoNumberGenerator = new PseudoNumberGenerator();
            //SynchronousStreamCipher synchronousStreamCipher = new SynchronousStreamCipher();
            //CiphertextAutokey ciphertextAutokey = new CiphertextAutokey();
            //string generatorResult = pseudoNumberGenerator.Generate("1001", "1001", word.Length);
            //string steamCipherResult = synchronousStreamCipher.Cipher(generatorResult, word);
            //string ciphertextAutokeyResult = ciphertextAutokey.Cipher("0010", "1001", "11101001");
            //string deciphertextAutokeyResult = ciphertextAutokey.Decipher("0010", "1001", ciphertextAutokeyResult);
            //Console.WriteLine("generator");
            //Console.WriteLine(generatorResult);
            //Console.WriteLine("streamcipher");
            //Console.WriteLine(steamCipherResult);
            //Console.WriteLine("cipherautokey");
            //Console.WriteLine(ciphertextAutokeyResult);
            //Console.WriteLine(deciphertextAutokeyResult);

        }

        private static void CiphertextAutokey()
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
                Console.WriteLine("Enter Seed value");
                string seed = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("Enter Polynomial value");
                string polynomial = Console.ReadLine();

                PseudoNumberGenerator pseudoNumberGenerator = new PseudoNumberGenerator();
                string generatorResult = pseudoNumberGenerator.Generate(seed, polynomial, word.Length);

                CiphertextAutokey ciphertextAutokey = new CiphertextAutokey();             
                string result;

                if (optionSelected == 0)
                    result = ciphertextAutokey.Cipher(seed, polynomial, word);
                else
                    result = ciphertextAutokey.Decipher(seed, polynomial, word);

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Result: " + result);
                Console.ForegroundColor = ConsoleColor.White;
                keyEntered = ReadKey();
            } while (keyEntered != ConsoleKey.Escape);
        }

        private static void SynchronousStreamCipherExecute()
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
                Console.WriteLine("Enter Seed value");
                string seed = Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("Enter Polynomial value");
                string polynomial = Console.ReadLine();
               
                SynchronousStreamCipher synchronousStreamCipher = new SynchronousStreamCipher();
                string result;
                result = synchronousStreamCipher.Cipher(word, seed, polynomial);

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Result: " + result); 
                Console.ForegroundColor = ConsoleColor.White;
                keyEntered = ReadKey();
            } while (keyEntered != ConsoleKey.Escape);

        }

        private static void NumberGenerator()
        {
            ConsoleKey keyEntered;
            int optionSelected = 0;

            do
            {
                Console.Clear();
                string[] options = { "Endless", "Limited" };
                showOptions(options, optionSelected);

                keyEntered = ReadKey();
                optionSelected = CheckArrowKey(keyEntered, optionSelected, options);

            } while (keyEntered != ConsoleKey.Enter && keyEntered != ConsoleKey.Escape);

            if (keyEntered == ConsoleKey.Escape)
                return;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter Seed value");
                string seed = Console.ReadLine();
                Console.WriteLine("Enter Polynomial value");
                string polynomial = Console.ReadLine();               
                int length = 0;             
                if (optionSelected == 1)
                {
                    Console.WriteLine("Enter length");
                    length = Int32.Parse(Console.ReadLine());
                }

                PseudoNumberGenerator pseudoNumberGenerator = new PseudoNumberGenerator();
                if (optionSelected == 0)
                    pseudoNumberGenerator.GenerateEndless(seed, polynomial);
                else
                {
                    List<string> result = pseudoNumberGenerator.GenerateList(seed, polynomial, length);
                    foreach(string row in result)
                    {
                        Console.WriteLine(row);
                    }
                }
                    


                keyEntered = ReadKey();
            } while (keyEntered != ConsoleKey.Escape);

            return;
            
        }

        private static void showOptions(string[] options, int optionSelected)
        {
            Console.WriteLine("");
            for (int i=0; i<options.Length; i++)
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
