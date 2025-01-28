using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using System;
using System.Diagnostics;

namespace SudokuSolver.UI
{
    /// <summary>
    /// A class which offers utilty functions for a sudoku solver UI.
    /// </summary>
    internal class SudokuUI
    {
        /// <summary>
        /// A method which stars the UI which receives a sudoku grid and returns its solution.
        /// </summary>
        public static void StartSudokuSolver()
        {
            DisableProgramTermination();
            PrintBlue(UIConstants.START_MESSAGE);
            SolveSudokus();
        }

        /// <summary>
        /// A method which handles receiving the input and returning the input until exit.
        /// </summary>
        private static void SolveSudokus()
        {
            Stopwatch stopwatch = new Stopwatch();
            string input = "";
            bool graphicMode = true;

            while (input is null || !input.Equals("exit"))
            {
                try
                {
                    InputHandler.GetInput(ref input, ref graphicMode);
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (!input.Equals("exit"))
                        ShowOutput(input, graphicMode);
                }
                catch (Exception ex) when (ex is IllegalStringOfSudokuGridException ||
                            ex is ArgumentException ||
                            ex is UnsolvableSudokuGridException)
                {
                    PrintRed(ex.Message);
                    if (ex is UnsolvableSudokuGridException)
                    {
                        stopwatch.Stop();
                        PrintGreen($"The algorithm took {stopwatch.ElapsedMilliseconds} ms.");
                    }
                }
            }
        }

        /// <summary>
        /// A method which disables keyboard shortcuts that might terminate the program.
        /// </summary>
        private static void DisableProgramTermination()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
            };
        }

        /// <summary>
        /// A method which shows the solution of a sudoku grid.
        /// </summary>
        private static void ShowOutput(string grid, bool graphicMode)
        {
            Stopwatch stopwatch = new Stopwatch();
            SudokuGridSolver solver = new SudokuGridSolver(grid);
            string solution;
            PrintGreen(UIConstants.RESULT_MESSAGE);
            stopwatch.Start();
            solution = solver.Solve();
            stopwatch.Stop();
            if (graphicMode)
                SudokuGridPrinter.PrintSudokuGrid(solution);
            else
                PrintGreen(solution);
            stopwatch.Stop();
            PrintGreen($"The algorithm took {stopwatch.ElapsedMilliseconds} ms.");
        }

        /// <summary>
        /// A method which prints a string in red.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
        }

        /// <summary>
        /// A method which prints a string in green.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
        }

        /// <summary>
        /// A method which prints a string in blue.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintBlue(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
        }
    }
}
