using System;

namespace SI_Lab_02
{
    class Program
    {
        public static int HORIZONTAL = 0;
        public static int VERTICAL = 1;

        static void Main(string[] args)
        {
            
            var sudoku = SudokuReader.ReadSudoku(41);

            SudokuReader.PrintSudoku(sudoku);

            var results = SudokuCSP.SolveSudoku2(sudoku);

            Console.WriteLine("Naciśnij 'y' jeśli chcesz wyświetlić znalezione rozwiązania");
            if(Console.ReadLine() == "y" && results!=null)
            {
                foreach (var element in results)
                {
                    SudokuReader.PrintSudoku(element);
                }

            }



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
