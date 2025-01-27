using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using System;
using System.Diagnostics;
using System.IO;

namespace SudokuSolver.UI
{
    /// <summary>
    /// A class which offers utilty functions for a sudoku solver UI.
    /// </summary>
    internal class SudokuUI
    {
        protected const int MAX_PARAMETERS = 2, MIN_PARAMETERS = 1;
        protected const string START_MESSAGE = "Welcome to the Omega Sudoku Solver.\n" +
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
            PrintBlue(START_MESSAGE);
            SolveSudokus();
        }

        private static void SolveSudokus()
        {
            Stopwatch stopwatch = new Stopwatch();
            string input = "";
            bool graphicMode = true;

            while (!input.Equals("exit"))
            {
                try
                {
                    GetInput(ref input, ref graphicMode);
                    if (!input.Equals("exit"))
                        ShowOutput(input, stopwatch, graphicMode);
                }
                catch (Exception ex) when (ex is IllegalStringOfSudokuGridException ||
                            ex is UnsolvableSudokuGridException ||
                            ex is ArgumentException)
                {
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

        private static void ShowOutput(string grid, Stopwatch stopwatch, bool graphicMode)
        {
            SudokuGridSolver solver = new SudokuGridSolver(grid);
            string solution;
            PrintGreen(RESULT_MESSAGE);
            stopwatch.Reset();
            stopwatch.Start();
            solution = solver.Solve();
            stopwatch.Stop();
            if (graphicMode)
                PrintSudokuGrid(solution);
            else
                PrintGreen(solution);
            PrintGreen($"It was solved in {stopwatch.ElapsedMilliseconds} ms.");
        }

        public static void GetInput(ref string input, ref bool graphicMode)
        {
            PrintBlue(MENU_MESSAGE);
            input = Console.ReadLine();
            input = HandleInput(input, ref graphicMode);
            Console.Write("\f\u001bc\x1b[3J");
            if (input.Equals("exit"))
                return;
            FileUI.HandleFile(ref input);
            SudokuGridValidator.ValidateLegalStringOfGrid(input);
            PrintBlue(SHOW_INPUT_MESSAGE);
            if (graphicMode)
                PrintSudokuGrid(input);
            else
                PrintBlue(input);
        }

        protected static string HandleInput(string input, ref bool graphicMode)
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

        public static void PrintSudokuGrid(string grid)
        {
            int gridSize = SudokuGridSolver.GetGridSize(grid);

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
