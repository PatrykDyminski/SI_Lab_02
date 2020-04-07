using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SI_Lab_02.Sudoku.CSP
{
    class BacktrackingAlterCSP
    {
        public static List<int[][]> SolveSudoku(int[][] problem, INextVariable nextVariable)
        {
            int nodesUntilFirst = 0;
            int reversesUntilFirst = 0;
            int nodesCount = 0;
            int reversesCount = 0;
            int solutionCount = 0;

            Stopwatch timer = Stopwatch.StartNew();

            bool isSolved = false;

            (bool solved, List<int[][]> solutions) = GetAllSolutions(problem);

            (bool isSolved, List<int[][]> solutions) GetAllSolutions(int[][] problem)
            {

                if (isSolved == false)
                    nodesUntilFirst++;
                var variable = nextVariable.Next(problem);
                if (variable.row == -1)
                {
                    isSolved = true;
                    solutionCount++;
                    return (true, new List<int[][]> { problem });
                }

                var solutions = new List<int[][]>();

                var filteredDomain = SudokuUtils.FilterDomain(problem, variable.row, variable.column);

                foreach (var value in filteredDomain)
                {
                    //value = PickNextValue(value);

                    nodesCount++;

                    if (SudokuUtils.CheckConstraint(problem, value, variable.row, variable.column))
                    {
                        int[][] newProblem = SudokuUtils.CopyArray(problem);
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

            timer.Stop();
            TimeSpan timespan = timer.Elapsed;

            Console.WriteLine("Czas działania (sek:milisek): " + String.Format("{0:00}:{1:00}", timespan.Seconds, timespan.Milliseconds / 10));
            Console.WriteLine("Odwiedzono do 1 rozwiązania: " + nodesUntilFirst);
            Console.WriteLine("Nawroty do 1 rozwiązania: " + reversesUntilFirst);
            Console.WriteLine("W sumie odwiedzono: " + nodesCount);
            Console.WriteLine("W sumie nawrotów: " + reversesCount);
            Console.WriteLine("Znaleziono rozwiązań: " + solutionCount);
            Console.WriteLine();

            return solutions;
        }
    }
}
