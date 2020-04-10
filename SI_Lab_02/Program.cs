using SI_Lab_02.Sudoku.CSP;
using System;

namespace SI_Lab_02
{
    class Program
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;

        static void Main(string[] args)
        {

            int nr = 45;

            var sudoku = SudokuReader.ReadSudoku(nr - 1);

            //SudokuReader.PrintSudoku(sudoku);

            var nextNatural = new NextVariableNatural();
            var nextWave = new NextVariableWave();
            var naturalOrder = new DomainOrderNatural();
            var randomOrder = new DomainOrderRandom();

            //var resTemp = SudokuCSP.SolveSudokuForwardRev(sudoku, nextNatural);

            Console.WriteLine("Backtracking, naturalna kolejność zmiennych, naturalna kolejność w dziedzinie"); 
            var bnn = BacktrackingCSP.SolveSudoku(sudoku, nextNatural, naturalOrder);

            Console.WriteLine("Forward Checking, naturalna kolejność zmiennych, naturalna kolejność w dziedzinie");
            var fnn = ForwardCheckingCSP.SolveSudoku(sudoku, nextNatural, naturalOrder);

            Console.WriteLine("Backtracking, falowa kolejność zmiennych, naturalna kolejność w dziedzinie");
            var bwn = BacktrackingCSP.SolveSudoku(sudoku, nextWave, naturalOrder);

            Console.WriteLine("Forward Checking, falowa kolejność zmiennych, naturalna kolejność w dziedzinie");
            var fwn = ForwardCheckingCSP.SolveSudoku(sudoku, nextWave, naturalOrder);

            Console.WriteLine("Backtracking, naturalna kolejność zmiennych, losowa kolejność w dziedzinie");
            var bnr = BacktrackingCSP.SolveSudoku(sudoku, nextNatural, randomOrder);

            Console.WriteLine("Forward Checking, naturalna kolejność zmiennych, losowa kolejność w dziedzinie");
            var fnr = ForwardCheckingCSP.SolveSudoku(sudoku, nextNatural, randomOrder);

            Console.WriteLine("Backtracking, falowa kolejność zmiennych, losowa kolejność w dziedzinie");
            var bwr = BacktrackingCSP.SolveSudoku(sudoku, nextWave, randomOrder);

            Console.WriteLine("Forward Checking, falowa kolejność zmiennych, losowa kolejność w dziedzinie");
            var fwr = ForwardCheckingCSP.SolveSudoku(sudoku, nextWave, randomOrder);


            //Console.WriteLine("Naciśnij 'y' jeśli chcesz wyświetlić znalezione rozwiązania");
            //if (Console.ReadLine() == "y" && results != null)
            //{
            //    foreach (var element in results)
            //    {
            //        SudokuReader.PrintSudoku(element);
            //    }

            //}

            //int[][] test = new int[9][];
            //test[0] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
            //test[1] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
            //test[2] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };

            //test[3] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
            //test[4] = new int[] { 0, 0, 0,  0, 5, 0,  0, 0, 0 };
            //test[5] = new int[] { 0, 0, 0,  0, 6, 7,  0, 0, 0 };

            //test[6] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
            //test[7] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
            //test[8] = new int[] { 0, 0, 0,  0, 0, 0,  0, 0, 0 };
        }
    }
}
