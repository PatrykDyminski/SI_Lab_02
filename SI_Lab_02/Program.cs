namespace SI_Lab_02
{
    class Program
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;

        static void Main(string[] args)
        {

            var sudoku = SudokuReader.ReadSudoku(41);

            //SudokuReader.PrintSudoku(sudoku);

            var nextNatural = new NextVariableNatural();
            var nextWave = new NextVariableWave();
            var naturalOrder = new DomainOrderNatural();
            var randomOrder = new DomainOrderRandom();

            //var resTemp = SudokuCSP.SolveSudokuForwardRev(sudoku, nextNatural);

            var results = SudokuCSP.SolveSudoku2(sudoku, nextNatural, naturalOrder);
            var resultsForward = SudokuCSP.SolveSudokuForwardRev(sudoku, nextNatural, naturalOrder);
            var results2 = SudokuCSP.SolveSudoku2(sudoku, nextWave, naturalOrder);
            var resultsForward2 = SudokuCSP.SolveSudokuForwardRev(sudoku, nextWave, naturalOrder);

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


            //

            //SudokuReader.PrintArray(domains);


            //SudokuCSP.PickNextVariable2(test);

            //SudokuReader.PrintArray(SudokuCSP.FilterDomain(test, 3, 3));

            //var words = JolkaReader.ReadWords(1);
            //var puzzle = JolkaReader.ReadPuzzle(1);

            //Console.WriteLine(result.Count);

            //foreach(var sudo in result)
            //{
            //    SudokuReader.PrintSudoku(sudo);
            //}

            //var variables = JolkaCSP.PopulateVariables(puzzle);

            //var results = JolkaCSP.SolveJolka(puzzle, words);

            //Console.WriteLine(words.Length);
            //Console.WriteLine(variables.Count);
            //foreach (var element in results)
            //{
            //JolkaReader.PrintPuzzle(element);
            //}

            //JolkaReader.PrintPuzzle(JolkaCSP.InsertInto(puzzle, 0, 0, 0, "XDD"));
        }
    }
}
