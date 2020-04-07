using System;
using System.Collections.Generic;
using System.Linq;

namespace SI_Lab_02
{
    class JolkaCSP
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;

        public static List<char[][]> SolveJolka(char[][] jolka, string[] words)
        {
            int nodesUntilFirst = 0;
            int reversesUntilFirst = 0;
            int nodesCount = 0;
            int reversesCount = 0;
            int solutionCount = 0;

            bool m_isSolved = false;

            List<Tuple<int, int, int>> variables = PopulateVariables(jolka);

            (bool solved, List<char[][]> solutions) = GetAllSolutions(jolka, words, variables);

            (bool isSolved, List<char[][]> solutions) GetAllSolutions(char[][] jolka, string[] words, List<Tuple<int, int, int>> variables)
            {
                nodesCount++;
                if (m_isSolved == false)
                    nodesUntilFirst++;
                var variable = (variables.Count>0) ? variables[0] : null;
                if (variable == null)
                {
                    m_isSolved = true;
                    solutionCount++;
                    //Console.WriteLine("dodano");
                    return (true, new List<char[][]> { jolka });
                }

                var solutions = new List<char[][]>();

                //var domain = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                foreach (var value in words)
                {
                    int row = variable.Item1;
                    int column = variable.Item2;
                    int direction = variable.Item3;

                    if (CheckConstraint(jolka, value, row, column, direction))
                    {

                        char[][] newProblem = copyArray(jolka);
                        ///!!!!!!!
                        newProblem = InsertInto(newProblem,row,column,direction,value);

                        if (variables.Count > 0) {
                            variables.RemoveAt(0);
                        }

                        var (isSolved, foundSolutions) = GetAllSolutions(newProblem, words, variables);
                        if (isSolved == true)
                        {
                            //Console.WriteLine("dodano rozwiazanie");
                            solutions.AddRange(foundSolutions);
                        }
                    }
                }

                reversesCount++;
                if (m_isSolved == false)
                    reversesUntilFirst++;

                return solutions.Any()
                ? (true, solutions)
                : (false, null);
            }

            Console.WriteLine(solutionCount);

            return solutions;


        }

        public static char[][] InsertInto(char[][] jolka, int row, int column, int direction, string word)
        {
            char[] charArray = word.ToCharArray();

            if (direction == VERTICAL)
            {
                for(int i = 0; i < word.Length; i++)
                {
                    jolka[row + i][column] = charArray[i];
                }
            }
            else if (direction == HORIZONTAL)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    jolka[row][column+i] = charArray[i];
                }
            }

            return jolka;
        }

        public static char[][] copyArray(char[][] source)
        {
            char[][] result = new char[source.Length][];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = new char[source[i].Length];

                for (int j = 0; j < source[i].Length; j++)
                {
                    result[i][j] = source[i][j];
                }
            }

            return result;
        }

        public static List<Tuple<int, int, int>> PopulateVariables(char[][] jolka)
        {
            List<Tuple<int, int, int>> variables = new List<Tuple<int, int, int>>();

            //wiersze
            for (int i = 0; i < jolka.Length; i++)
            {
                char[] line = jolka[i];
                var pierwszePole = line[0];

                if (pierwszePole.Equals('_'))
                {
                    if(detectLengthOk(jolka, i, 0, HORIZONTAL))
                    {
                        variables.Add(Tuple.Create(i, 0, HORIZONTAL));
                    }
                }

                for(int j = 1; j< jolka[i].Length; j++)
                {
                    if (jolka[i][j-1].Equals('#') && jolka[i][j].Equals('_'))
                    {
                        if (detectLengthOk(jolka, i, j, HORIZONTAL))
                        {
                            variables.Add(Tuple.Create(i, j, HORIZONTAL));
                        }
                    }
                }
            }

            //kolumny
            for (int i = 0; i < jolka[0].Length; i++)
            {
                char[] column = getColumn(jolka, i);
                var pierwszePole = column[0];

                if (pierwszePole.Equals('_'))
                {
                    if (detectLengthOk(jolka, 0, i, VERTICAL))
                    {
                        variables.Add(Tuple.Create(0, i, VERTICAL));
                    }
                }

                for (int j = 1; j < jolka.Length; j++)
                {
                    if (jolka[j - 1][i].Equals('#') && jolka[j][i].Equals('_'))
                    {
                        if (detectLengthOk(jolka, j, i, VERTICAL))
                        {
                            variables.Add(Tuple.Create(j, i, VERTICAL));
                        }
                    }
                }
            }
            return variables;
        }

        public static char[] getColumn(char[][] jolka, int column)
        {
            char[] charColumn = new char[jolka.Length];

            for (int i = 0; i < jolka.Length; i++)
            {
                charColumn[i] = jolka[i][column];
            }

            return charColumn;
        }

        public static bool detectLengthOk(char[][] jolka, int row, int column, int direction)
        {
            if (direction == VERTICAL)
            {
                if (row != (jolka.Length - 1))
                {
                    if (!jolka[row+1][column].Equals('#'))
                    {
                        return true;
                    }
                }
            }
            else if (direction == HORIZONTAL)
            {
                if (column != (jolka[0].Length - 1))
                {
                    if(!jolka[row][column + 1].Equals('#'))
                    {
                        return true;
                    }
                }      
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }

            return false;
        }

        public static bool CheckConstraint(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            if (CheckLength(puzzle, word, startRow, startColumn, direction))
            {
                if (CheckFill(puzzle, word, startRow, startColumn, direction))
                {
                    if (CheckLetters(puzzle, word, startRow, startColumn, direction))
                    {
                        return true;
                    }
                    else
                    {
                        //Console.WriteLine("Litery się nie zgadzają");
                        return false;
                    }
                }
                else
                {
                    //Console.WriteLine("Wypełnienie się nie zgadza");
                    return false;
                }
            }
            else
            {
                //Console.WriteLine("Wolna przestrzeń się nie zgadza");
                return false;
            }
        }

        public static bool CheckFill(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            if (direction == VERTICAL)
            {
                if (startRow != 0)
                {
                    if (!puzzle[startRow - 1][startColumn].Equals('#'))
                    {
                        return false;
                    }
                }

                if (startRow + word.Length != puzzle.Length)
                {
                    if (!puzzle[startRow + word.Length][startColumn].Equals('#'))
                    {
                        return false;
                    }
                }
            }
            else if (direction == HORIZONTAL)
            {
                if (startColumn != 0)
                {
                    if (!puzzle[startRow][startColumn-1].Equals('#'))
                    {
                        return false;
                    }
                }

                if (startColumn + word.Length != puzzle[0].Length)
                {
                    if (!puzzle[startRow][startColumn + word.Length].Equals('#'))
                    {
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }

            return true;
        }

        public static bool CheckLength(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            if(puzzle.Length <= startRow || puzzle[0].Length <= startColumn)
            {
                Console.WriteLine("Poza zakresem");
                return false;
            }

            if(direction == VERTICAL)
            {
                return (puzzle.Length - startRow >= word.Length) ? true : false;
            }
            else if(direction == HORIZONTAL)
            {
                return (puzzle[0].Length - startColumn >= word.Length) ? true : false;
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }

        }

        public static bool CheckLetters(char[][] puzzle, string word, int startRow, int startColumn, int direction)
        {
            char[] wordChars = new char[word.Length];
            wordChars = word.ToCharArray();

            if (direction == VERTICAL)
            {
                for(int i = 0; i< word.Length; i++)
                {
                    if (!puzzle[startRow + i][startColumn].Equals('_'))
                    {
                        if ( !wordChars[i].Equals(puzzle[startRow + i][startColumn]) || puzzle[startRow + i][startColumn].Equals('#'))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else if (direction == HORIZONTAL)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (!puzzle[startRow][startColumn + i].Equals('_'))
                    {
                        if (!wordChars[i].Equals(puzzle[startRow][startColumn + i]) || puzzle[startRow][startColumn + i].Equals('#'))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            else
            {
                Console.WriteLine("No coś namieszałeś!");
                return false;
            }
        }

    }
}
