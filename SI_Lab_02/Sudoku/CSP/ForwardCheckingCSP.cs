using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SI_Lab_02.Sudoku.CSP
{
    class ForwardCheckingCSP
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

            List<int>[][] initDomains = new List<int>[9][]; ;


            //inicjalizacja dzieddzin
            for (int i = 0; i < 9; i++)
            {
                initDomains[i] = new List<int>[9];
                for (int j = 0; j < 9; j++)
                {
                    initDomains[i][j] = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                }
            }

            //wykluczenie z dziedzin elementów startowych sudoku
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (problem[i][j] != 0)
                    {
                        initDomains = SudokuUtils.UpdateDomains(initDomains, i, j, problem[i][j]);
                    }
                }
            }

            (bool solved, List<int[][]> solutions) = GetAllSolutions(problem, initDomains);

            (bool isSolved, List<int[][]> solutions) GetAllSolutions(int[][] problem, List<int>[][] domains)
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

                var domain = domainOrder.GetOrder(domains[variable.row][variable.column]);

                foreach (var value in domain)
                {
                    nodesCount++;

                    int[][] newProblem = SudokuUtils.CopyArray(problem);
                    newProblem[variable.row][variable.column] = value;

                    var newDomain = SudokuUtils.UpdateDomains(domains, variable.row, variable.column, value);

                    //if (CountEmptyDomains(newDomain) <= CountNonZeroFields(newProblem))
                    if (!SudokuUtils.EmptyDomainAndFieldUnassigned(newProblem, newDomain))
                    {
                        var (isSolved, foundSolutions) = GetAllSolutions(newProblem, newDomain);
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
