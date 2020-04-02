using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Lab_02
{
    interface INextVariable
    {
        public (int row, int column) Next(int[][] sudoku);
    }
}
