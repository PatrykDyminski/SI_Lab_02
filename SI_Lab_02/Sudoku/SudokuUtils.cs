using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SI_Lab_02
{
    class SudokuUtils
    {      
        public static bool ExistEmptyDomain(List<int>[][] domains)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (domains[i][j].Count == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static int CountEmptyDomains(List<int>[][] domains)
        {
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (domains[i][j].Count == 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static int CountNonZeroFields(int[][] sudoku)
        {
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i][j] != 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static bool EmptyDomainAndFieldUnassigned(int[][] sudoku, List<int>[][] domains)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((domains[i][j].Count == 0) && (sudoku[i][j] == 0))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static List<int>[][] UpdateDomains(List<int>[][] domains, int row, int column, int value)
        {

            List<int>[][] domainsNew = new List<int>[9][];

            for (int i = 0; i < 9; i++)
            {
                domainsNew[i] = new List<int>[9];

                for(int j = 0; j<9; j++)
                {
                    domainsNew[i][j] = new List<int>();

                    domains[i][j].ForEach((item) =>
                    {
                        domainsNew[i][j].Add(item);
                    });
                }

            }

            for (int i = 0; i < 9; i++)
            {
                domainsNew[row][i].Remove(value);
            }

            for (int i = 0; i < 9; i++)
            {
                domainsNew[i][column].Remove(value);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    domainsNew[i + row - row % 3][j + column - column % 3].Remove(value);
                }
            }

            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        Console.Write(i + " " + j + " ");
            //        domains[i][j].ForEach(Console.Write);
            //        Console.WriteLine();
            //    }
            //}

            return domainsNew;
        }

        public static int[] FilterDomain(int[][] sudoku, int row, int column)
        {
            List<int> badDomain = new List<int>();
            List<int> fullDomain = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 9; i++)
            {
                badDomain.Add(sudoku[row][i]);
            }

            for (int i = 0; i < 9; i++)
            {
                badDomain.Add(sudoku[i][column]);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    badDomain.Add(sudoku[i + row - row % 3][j + column - column % 3]);
                }
            }

            return fullDomain.Except(badDomain.Distinct()).ToArray();

        }

        public static bool CheckConstraint(int[][] sudoku, int number, int row, int column)
        {
            return
            CheckNumberValue(number) &&
            CheckRow(sudoku, number, row) &&
            CheckColumn(sudoku, number, column) &&
            CheckBox(sudoku, number, row - row % 3, column - column % 3);
            //&& sudoku[row][column] == 0;
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

        public static int[][] CopyArray(int[][] source)
        {
            int[][] result = new int[source.Length][];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = new int[source[i].Length];

                for (int j = 0; j < source[i].Length; j++)
                {
                    result[i][j] = source[i][j];
                }
            }

            return result;
        }

    }
}
