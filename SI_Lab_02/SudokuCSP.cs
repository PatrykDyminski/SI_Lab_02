using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    class SudokuCSP
    {

        public static void SolveSudoku(int[][] sudoku)
        {

            int prevRow = 0;
            int prevColumn = 0;

            (int row, int column) = PickNextValue(sudoku);

            if(row == -1)
            {
                Console.WriteLine("Znaleziono rozwiązanie");
                return;
            }
            else
            {
                int currentPick = -1;

                currentPick = PickNextPick(currentPick);

                if(currentPick == -100)
                {



                }




            }

        }

        private static int PickNextPick(int currentPick)
        {
            return (currentPick == 9) ? -100 : currentPick++;
        }

        private static (int row, int column) PickNextValue(int[][] sudoku)
        {
            bool ok = false;

            int row = -1;
            int column = -1;


            for(int i = 0; i< 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(sudoku[i][j] == 0)
                    {
                        return (i, j);
                    }
                }
            }

            if(row == -1)
            {
                return (row, column);
            }



            //while (!ok)
            //{
            //    Random rnd = new Random();
            //    row = rnd.Next(0, 9);
            //    column = rnd.Next(0, 9);

            //    if(sudoku[row][column] == 0)
            //    {
            //        ok = true;
            //    }
            //}

            return (row, column);

        }

        public static bool CheckConstraint(int[][] sudoku, int number, int row, int column)
        {
            return
            CheckNumberValue(number) &&
            CheckRow(sudoku, number, row) && 
            CheckColumn(sudoku, number, column) &&
            CheckBox(sudoku, number, row - row % 3, column - column % 3) && 
            sudoku[row][column] == 0;
        }


        public static bool CheckNumberValue(int number)
        {
            return (number <= 9 && number > 0) ? true : false;
        }

        public static bool CheckRow(int[][] sudoku, int number, int row)
        {
            for(int i = 0; i < 9; i++)
            {
                if(sudoku[row][i] == number)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckColumn(int[][] sudoku, int number, int column)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i][column] == number)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckBox(int[][] sudoku, int number, int boxi, int boxj)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (sudoku[row + boxi][col + boxj] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}
