using System;

namespace SI_Lab_02
{
    class Program
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;

        static void Main(string[] args)
        {
            
            //var sudoku = SudokuReader.ReadSudoku(0);

            //JolkaReader.ReadWords(0);
            var puzzle = JolkaReader.ReadPuzzle(1);

            puzzle[0][2] = 'b';
            puzzle[0][3] = 'e';
            puzzle[0][4] = 'e';

            var result = JolkaCSP.CheckConstraint(puzzle, "bee", 0, 2, HORIZONTAL);




            //var result = SudokuCSP.CheckConstraint(sudoku, 9, 0, 0);
            Console.WriteLine(result);
        }
    }
}
