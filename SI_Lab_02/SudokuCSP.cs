﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI_Lab_02
{
    class SudokuCSP
    {
        public static List<int[][]> SolveSudoku2(int[][] problem)
        {
            int nodesUntilFirst = 0;
            int reversesUntilFirst = 0;
            int nodesCount = 0;
            int reversesCount = 0;
            int solutionCount = 0;

            bool isSolved = false;

            (bool solved, List<int[][]> solutions) = GetAllSolutions(problem);

            (bool isSolved, List<int[][]> solutions) GetAllSolutions(int[][] problem)
            {
                
                if (isSolved == false)
                    nodesUntilFirst++;
                var variable = PickNextVariable(problem);
                if (variable.row == -1)
                {
                    isSolved = true;
                    solutionCount++;
                    return (true, new List<int[][]> { problem });
                }

                var solutions = new List<int[][]>();

                //var domain = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                int value = 0;

                //foreach (var value in domain)
                while(value != -1)
                {
                    value = PickNextValue(value);

                    nodesCount++;

                    if (CheckConstraint(problem, value, variable.row, variable.column))
                    {
                        int[][] newProblem = copyArray(problem);
                        newProblem[variable.row][variable.column] = value;

                        var (isSolved, foundSolutions) = GetAllSolutions(newProblem);
                        if (isSolved == true)
                        {
                            solutions.AddRange(foundSolutions);
                        }
                    }
                }

                reversesCount++;
                if (isSolved == false)
                {
                    reversesUntilFirst++;
                }
                    

                return solutions.Any() ? (true, solutions) : (false, null);
            }

            Console.WriteLine("Odwiedzono do 1 rozwiązania: " + nodesUntilFirst);
            Console.WriteLine("Nawroty do 1 rozwiązania: " + reversesUntilFirst);
            Console.WriteLine("W sumie odwiedzono: " + nodesCount);
            Console.WriteLine("W sumie nawrotów: " + reversesCount);
            Console.WriteLine("Znaleziono rozwiązań: " + solutionCount);

            return solutions;
        }

        public static List<int[][]> SolveSudokuForward(int[][] problem)
        {
            int nodesUntilFirst = 0;
            int reversesUntilFirst = 0;
            int nodesCount = 0;
            int reversesCount = 0;
            int solutionCount = 0;

            bool isSolved = false;

            (bool solved, List<int[][]> solutions) = GetAllSolutions(problem);

            (bool isSolved, List<int[][]> solutions) GetAllSolutions(int[][] problem)
            {
                
                if (isSolved == false)
                    nodesUntilFirst++;
                var variable = PickNextVariable(problem);
                if (variable.row == -1)
                {
                    isSolved = true;
                    solutionCount++;
                    return (true, new List<int[][]> { problem });
                }

                var solutions = new List<int[][]>();

                //var domain = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var filteredDomain = FilterDomain(problem, variable.row, variable.column);

                //int value = 0;

                //while (value != -1)
                foreach (var value in filteredDomain)
                {
                    //value = PickNextValue(value);

                    nodesCount++;

                    if (CheckConstraint(problem, value, variable.row, variable.column))
                    {
                        int[][] newProblem = copyArray(problem);
                        newProblem[variable.row][variable.column] = value;

                        var (isSolved, foundSolutions) = GetAllSolutions(newProblem);
                        if (isSolved == true)
                        {
                            solutions.AddRange(foundSolutions);
                        }
                    }
                }

                reversesCount++;
                if (isSolved == false)
                {
                    reversesUntilFirst++;
                }


                return solutions.Any() ? (true, solutions) : (false, null);
            }

            Console.WriteLine("Odwiedzono do 1 rozwiązania: " + nodesUntilFirst);
            Console.WriteLine("Nawroty do 1 rozwiązania: " + reversesUntilFirst);
            Console.WriteLine("W sumie odwiedzono: " + nodesCount);
            Console.WriteLine("W sumie nawrotów: " + reversesCount);
            Console.WriteLine("Znaleziono rozwiązań: " + solutionCount);

            return solutions;
        }

        public static int[][] copyArray(int[][] source)
        {
            int[][] result = new int[source.Length][];

            for(int i = 0; i< source.Length; i++)
            {
                result[i] = new int[source[i].Length];

                for (int j = 0; j < source[i].Length; j++)
                {
                    result[i][j] = source[i][j];
                }
            }

            return result;
        }

        //stara metoda - znajduje jedno rozwiązanie
        public static List<int[][]> SolveSudoku(int[][] sudoku)
        {
            List<int[][]> solutions = new List<int[][]>();
            int counter = 0;

            SolveSudokuInner(sudoku);

            bool SolveSudokuInner(int[][] sudoku)
            {
                (int row, int col) = PickNextVariable(sudoku);

                if (row == -1)
                {
                    solutions.Add(sudoku);
                    counter++;
                    //SudokuReader.PrintSudoku(sudoku);
                    return true; // udane 
                }


                for (int num = 1; num <= 9; num++)
                {
                    if (CheckConstraint(sudoku, num, row, col))
                    {
                        sudoku[row][col] = num;
                        if (SolveSudokuInner(sudoku))
                            return true;

                        sudoku[row][col] = 0;
                    }
                }

                return false;
            }

            Console.WriteLine(counter);

            return solutions;
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

        private static int PickNextValue(int currentPick)
        {
            return currentPick != 9 ? ++currentPick : -1;
        }

        private static (int row, int column) PickNextVariable(int[][] sudoku)
        {
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
            return (row, column);
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


    }
}
