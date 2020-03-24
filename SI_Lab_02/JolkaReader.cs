using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SI_Lab_02
{
    class JolkaReader
    {

        public static char[][] ReadPuzzle(int i)
        {
            var lines = File.ReadAllLines("puzzle" + i);

            char[][] tablicaCzarow = new char[lines.Length][];

            //Console.WriteLine(lines.Length);

            for (int j = 0; j < lines.Length; j++)
            {

                tablicaCzarow[j] = new char[lines[j].Length];
                tablicaCzarow[j] = lines[j].ToCharArray();
            }

            PrintPuzzle(tablicaCzarow);

            return tablicaCzarow;
        }

        public static string[] ReadWords(int i)
        {
            return File.ReadAllLines("words" + i);
        }

        public static void PrintCharArray(char[] line)
        {
            foreach(var letter in line)
            {
                Console.Write(letter);
            }
            Console.WriteLine();
        }

        public static void PrintPuzzle(char[][] puzzle)
        {
            Console.WriteLine("Puzzle:");
            foreach (var line in puzzle)
            {
                PrintCharArray(line);
            }
            Console.WriteLine("End of Puzz - le");
        }

    }
}
