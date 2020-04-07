using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SI_Lab_02
{
    class SudokuCSP
    {

        //Backtracking
        public static List<int[][]> SolveSudoku2(int[][] problem, INextVariable nextVariable, IDomainOrder domainOrder)
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


        //Niepoprawny forward checking, tak naprawdę turbobacktracking XD
        public static List<int[][]> SolveSudokuForward(int[][] problem, INextVariable nextVariable)
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


        //Poprawny forward checking
        public static List<int[][]> SolveSudokuForwardRev(int[][] problem, INextVariable nextVariable, IDomainOrder domainOrder)
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
                        initDomains = UpdateDomains(initDomains, i, j, problem[i][j]);
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

                    int[][] newProblem = copyArray(problem);
                    newProblem[variable.row][variable.column] = value;

                    var newDomain = UpdateDomains(domains, variable.row, variable.column, value);

                    //if (CountEmptyDomains(newDomain) <= CountNonZeroFields(newProblem))
                    if (!EmptyDomainAndFieldUnassigned(newProblem, newDomain))
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

        public static int[][] copyArray(int[][] source)
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
