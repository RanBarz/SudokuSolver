using SudokuSolver.Core;
using System;
using System.Diagnostics;

namespace SudokuSolver.UI
{
    internal class ConsoleUI
    {
        private const string MENU_MESSAGE = "Enter a Sudoku grid (or 'exit'):";
        private const string RESULT_MESSAGE = "The solution to this grid is: ";
        private const string SHOW_INPUT_MESSAGE = 
            "The grid you entered looks as follows: ";

        public static void StartSudokuSolver()
        {
            SudokuGridSolver solver;
            Stopwatch stopwatch = new Stopwatch();
            string input;

            Console.WriteLine(MENU_MESSAGE);
            input = Console.ReadLine(); 

            while (input != "exit")
            {
                Console.WriteLine(SHOW_INPUT_MESSAGE);
                PrintSudokuGrid(input);
                solver = new SudokuGridSolver(input);
                Console.WriteLine(RESULT_MESSAGE);
                stopwatch.Reset();
                stopwatch.Start();
                PrintSudokuGrid(solver.Solve());
                stopwatch.Stop();
                Console.WriteLine($"It was solved in {stopwatch.ElapsedMilliseconds} ms.");
                Console.WriteLine(MENU_MESSAGE);
                input = Console.ReadLine();
            }
        }

        public static void PrintSudokuGrid(string grid)
        {
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(grid[index++] + " ");
                }
                Console.WriteLine("\n");
            }
        }
        public static void PrintRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public static void PrintGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
    }
}
