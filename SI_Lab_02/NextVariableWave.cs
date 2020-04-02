using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    class NextVariableWave : INextVariable
    {
        public (int row, int column) Next(int[][] sudoku)
        {
            List<Tuple<int, int>> puste = new List<Tuple<int, int>>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i][j] == 0)
                    {
                        puste.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            puste.Sort(CompareTuple);

            if (puste.Count == 0)
            {
                return (-1, -1);
            }

            //puste.ForEach(Console.WriteLine);

            return (puste[0].Item1, puste[0].Item2);
        }

        private static int CompareTuple(Tuple<int, int> t1, Tuple<int, int> t2)
        {
            if (t1.Item1 + t1.Item2 > t2.Item1 + t2.Item2)
            {
                return 1;
            }
            else if (t1.Item1 + t1.Item2 == t2.Item1 + t2.Item2)
            {
                return 0;
            }
            else return -1;
        }
    }
}
