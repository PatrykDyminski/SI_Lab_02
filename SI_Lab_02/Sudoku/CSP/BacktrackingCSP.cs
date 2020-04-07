using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SI_Lab_02.Sudoku.CSP
{
    class BacktrackingCSP
    {
        public static List<int[][]> SolveSudoku(int[][] problem, INextVariable nextVariable, IDomainOrder domainOrder)
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

                var dom = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var domain = domainOrder.GetOrder(dom.ToList());

                //int value = 0;

                foreach (var value in domain)
                //while(value != -1)
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
