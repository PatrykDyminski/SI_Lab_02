namespace SI_Lab_02
{
    class NextVariableNatural : INextVariable
    {
        public (int row, int column) Next(int[][] sudoku)
        {
            int row = -1;
            int column = -1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i][j] == 0)
                    {
                        return (i, j);
                    }
                }
            }
            if (row == -1)
            {
                return (row, column);
            }
            return (row, column);
        }
    }
}
