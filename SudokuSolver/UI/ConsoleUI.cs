using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
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

        public static void StartSudokuSolver(int gridSize)
        {
            SudokuGridSolver solver = null;
            Stopwatch stopwatch = new Stopwatch();
            string input = ""; 

            while (input != "exit")
            {
                try
                {
                    solver = GetInput(input, solver, gridSize);
                    ShowOutput(solver, stopwatch);
                }
                catch (IllegalStringOfSudokuGridException ex)
                {
                    PrintRed(ex.Message);
                }
            }
        }

        private static void ShowOutput(SudokuGridSolver solver, Stopwatch stopwatch)
        {
            PrintGreen(RESULT_MESSAGE);
            stopwatch.Reset();
            stopwatch.Start();
            PrintSudokuGrid(solver.Solve());
            stopwatch.Stop();
            PrintGreen($"It was solved in {stopwatch.ElapsedMilliseconds} ms.");
        }

        private static SudokuGridSolver GetInput(string input, SudokuGridSolver solver, int gridSize)
        {
            PrintBlue(MENU_MESSAGE);
            input = Console.ReadLine();
            if (!SudokuGridValidator.LegalStringOfGrid(input, gridSize))
                throw new IllegalStringOfSudokuGridException();
            PrintBlue(SHOW_INPUT_MESSAGE);
            PrintSudokuGrid(input);
            solver = new SudokuGridSolver(input);
            return solver;
        }

        public static void PrintSudokuGrid(string grid)
        {
            Console.WriteLine("╔═══════╤═══════╤═══════╗");

            for (int row = 0; row < 9; row++)
            {
                Console.Write("║ ");
                for (int col = 0; col < 9; col++)
                {
                    // Print the current number
                    char value = grid[row * 9 + col];
                    Console.Write(value == '0' ? '·' : value);

                    // Add spacing and vertical borders
                    if (col < 8)
                    {
                        Console.Write(" ");
                        if ((col + 1) % 3 == 0)
                        {
                            Console.Write("│ ");
                        }
                    }
                }
                Console.WriteLine(" ║");

                // Add horizontal borders between 3x3 sections
                if (row < 8 && (row + 1) % 3 == 0)
                {
                    Console.WriteLine("╟───────┼───────┼───────╢");
                }
            }

            Console.WriteLine("╚═══════╧═══════╧═══════╝");
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

        public static void PrintBlue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
        }
    }
}
