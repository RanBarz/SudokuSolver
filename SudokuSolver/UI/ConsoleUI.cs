using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using System;
using System.Diagnostics;
using System.IO;

namespace SudokuSolver.UI
{
    internal class ConsoleUI
    {
        private const int MAX_PARAMETERS = 2, MIN_PARAMETERS = 1;
        private const string START_MESSAGE = "Welcome to the Omega Sudoku Solver.\n" +
            "Instructions:\n" +
            "-\tEnter a string that is 81 characters long.\n" +
            "-\tEach character should be a digit between 0 and 9.\n" +
            "-\tZero represents an empty cell.\n" +
            "-\tEvery nine cells represent a row (from the top down).\n"
        , MENU_MESSAGE = "\nEnter a Sudoku grid (or 'exit', " +
            "you can add ' -s' for string representation):"
        , RESULT_MESSAGE = "\nThe solution to this grid is: "
        , SHOW_INPUT_MESSAGE = "The grid you entered looks as follows: ";

        public static void StartSudokuSolver()
        {
            DisableProgramTermination();

            int gridSize = 0;
            SudokuGridSolver solver = null;
            Stopwatch stopwatch = new Stopwatch();
            string input = "";
            bool graphicMode = true, exceptionThrown = false;
            PrintBlue(START_MESSAGE);

            while (solver != null || input != null && input.Equals("") || exceptionThrown)
            {
                exceptionThrown = false;
                try
                {
                    solver = GetInput(ref input, ref gridSize, ref graphicMode);
                    if (solver != null)
                        ShowOutput(solver, stopwatch, graphicMode, gridSize);
                }
                catch (Exception ex) when (ex is IllegalStringOfSudokuGridException ||
                            ex is UnsolvableSudokuGridException ||
                            ex is ArgumentException)
                {
                    exceptionThrown = true;
                    PrintRed(ex.Message);
                }
            }
        }

        private static void DisableProgramTermination()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
            };
        }

        private static void ShowOutput(SudokuGridSolver solver, Stopwatch stopwatch, bool graphicMode, 
            int gridSize)
        {
            string solution;
            PrintGreen(RESULT_MESSAGE);
            stopwatch.Reset();
            stopwatch.Start();
            solution = solver.Solve();
            stopwatch.Stop();
            if (graphicMode)
                PrintSudokuGrid(solution, gridSize);
            else
                PrintGreen(solution);
            PrintGreen($"It was solved in {stopwatch.ElapsedMilliseconds} ms.");
        }

        private static SudokuGridSolver GetInput(ref string input, 
            ref int gridSize, ref bool graphicMode)
        {
            SudokuGridSolver solver;
            PrintBlue(MENU_MESSAGE);
            input = Console.ReadLine();
            input = HandleInput(input, ref graphicMode);
            Console.Clear();
            if (input.Equals("exit"))
                return null;
            SudokuGridValidator.ValidateLegalStringOfGrid(input);
            gridSize = SudokuGridSolver.GetGridSize(input);
            PrintBlue(SHOW_INPUT_MESSAGE);
            if (graphicMode)
                PrintSudokuGrid(input, gridSize);
            else
                PrintBlue(input);
            solver = new SudokuGridSolver(input);
            return solver;
        }

        private static string HandleInput(string input, ref bool graphicMode)
        {
            string[] parameters;

            SudokuGridValidator.ValidateInput(input);
            parameters = input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            SudokuGridValidator.ValidateParameters(parameters, MAX_PARAMETERS, MIN_PARAMETERS);
            input = parameters[0];
            if (parameters.Length > 1)
            {
                if (parameters[1] == "-s")
                    graphicMode = false;
                else
                    throw new ArgumentException("The only acceptable second param is -s.");
            }
            else
                graphicMode = true;
            return input;
        }

        public static void PrintSudokuGrid(string grid, int gridSize)
        {
            PrintSudokuGridBorder(gridSize, true);

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write("║ ");
                for (int col = 0; col < gridSize; col++)
                {
                    char value = grid[row * gridSize + col];
                    Console.Write(value == '0' ? '·' : value);

                    if (col < gridSize - 1)
                    {
                        Console.Write(" ");
                        if ((col + 1) % Math.Sqrt(gridSize) == 0)
                        {
                            Console.Write("│ ");
                        }
                    }
                }
                Console.WriteLine(" ║");

                if (row < gridSize - 1 && (row + 1) % Math.Sqrt(gridSize) == 0)
                {
                    PrintSudokuSubgridBorder(gridSize);
                }
            }
            PrintSudokuGridBorder(gridSize, false);
        }

        public static void PrintSudokuGridBorder(int gridSize, bool isUpperBorder)
        {
            int subgridSize = (int) Math.Sqrt(gridSize);
            if (isUpperBorder)
                Console.Write("╔");
            else
                Console.Write("╚");
            for (int i = 0; i < (subgridSize * 2 + 1) * subgridSize + subgridSize - 1; i++)
                if (i % subgridSize * 2 + 2 == 0)
                    if (isUpperBorder)
                        Console.Write("╤");
                    else
                        Console.Write("╧");
                else
                    Console.Write("═");
            if (isUpperBorder)
                Console.WriteLine("╗");
            else
                Console.WriteLine("╝");
        }

        public static void PrintSudokuSubgridBorder(int gridSize)
        {
            int subgridSize = (int)Math.Sqrt(gridSize);

            Console.Write("╟");
            for (int i = 0; i < (subgridSize * 2 + 1) * subgridSize + subgridSize - 1; i++)
                if (i % subgridSize * 2 + 2 == 0)
                    Console.Write("┼");
                else
                    Console.Write("─");
            Console.WriteLine("╢");
        }


        public static void PrintRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
        }

        public static void PrintGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
        }

        public static void PrintBlue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
        }
    }
}
