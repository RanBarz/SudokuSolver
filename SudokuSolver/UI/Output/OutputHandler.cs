using SudokuSolver.Core;
using System;
using System.Diagnostics;

namespace SudokuSolver.UI.Output
{
    internal class OutputHandler
    {
        /// <summary>
        /// A method which shows the solution of a sudoku grid.
        /// </summary>
        public static void ShowOutput(string grid, bool graphicMode)
        {
            Stopwatch stopwatch = new Stopwatch();
            SudokuGridSolver solver = new SudokuGridSolver(grid);
            string solution;
            PrintGreen(UIConstants.ResultMessage);
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
