using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SI_Lab_02
{
    class SudokuReader
    {

        public static void ReadSudoku(int number)
        {
            var read = File.ReadAllLines("Sudoku.csv").Select(a => a.Split(';'));
            var lines = read.ToArray();
            string sudoku = lines[number][2];
            var parsed = ParseSudoku(sudoku);

            PrintSudoku(parsed);
        }

        public static int[][] ParseSudoku(string input)
        {
            var array = input.ToArray();
            int[] intArray = new int[81];

            for(int i = 0; i < 81; i++)
            {
                //Console.WriteLine(array[i]);

                if (array[i] == '.')
                {
                    intArray[i] = 0;
                }
                else
                {
                    intArray[i] = int.Parse(array[i].ToString());
                }
            }

            int[][] sudoku = new int[9][];
            int counter = 0;

            for(int i = 0; i<9; i++)
            {
                sudoku[i] = new int[9];

                for(int j = 0; j<9; j++)
                {
                    sudoku[i][j] = intArray[counter++];
                }
            }

            return sudoku;

        }

        public static void PrintArray(int[] line)
        {
            for (int i1 = 0; i1 < line.Length; i1++)
            {
                int i = line[i1];
                Console.Write(i + " ");

                if(i1 == 2 || i1 == 5 || i1 == 8)
                {
                    Console.Write("| ");
                }

            }
            Console.WriteLine();
        }

        public static void PrintSudoku(int[][] sudoku)
        {
            Console.WriteLine("Sudoku:");
            for (int i = 0; i < sudoku.Length; i++)
            {
                int[] line = sudoku[i];
                PrintArray(line);

                if (i == 2 || i == 5 || i == 8)
                {
                    Console.WriteLine("-----------------------");
                }

            }
            Console.WriteLine("End of SUDO-ku");
        }

    }
            
}
