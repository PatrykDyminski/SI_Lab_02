using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    class SudokuCSP
    {
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
