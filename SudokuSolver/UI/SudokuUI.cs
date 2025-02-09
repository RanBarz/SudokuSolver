using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.UI.Input;
using SudokuSolver.UI.Output;
using System;
using System.Diagnostics;

namespace SudokuSolver.UI
{
    /// <summary>
    /// A class which offers utilty functions for a sudoku solver UI.
    /// </summary>
    public class SudokuUI
    {
        /// <summary>
        /// A method which stars the UI which receives a sudoku grid and returns its solution.
        /// </summary>
        public static void StartSudokuSolver()
        {
            DisableProgramTermination();
            OutputHandler.PrintBlue(UIConstants.StartMessage);
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
                TrySudokuSolver(stopwatch, ref input, ref graphicMode);
            }
        }

        /// <summary>
        /// Get input for the Sudoku, try solving and show output.
        /// </summary>
        private static void TrySudokuSolver(Stopwatch stopwatch, ref string input, ref bool graphicMode)
        {
            try
            {
                InputHandler.GetInput(ref input, ref graphicMode);
                stopwatch.Reset();
                stopwatch.Start();
                if (!input.Equals("exit"))
                    OutputHandler.ShowOutput(input, graphicMode);
            }
            catch (Exception ex) when (ex.GetType() == typeof(IllegalStringOfSudokuGridException) ||
                        ex.GetType() == typeof(ArgumentException) ||
                        ex.GetType() == typeof(UnsolvableSudokuGridException) ||
                        ex.GetType() == typeof(InvalidSudokuGridException))
            {
                OutputHandler.PrintRed(ex.Message);
                if (ex is UnsolvableSudokuGridException)
                {   
                    stopwatch.Stop();
                    OutputHandler.PrintGreen($"The algorithm took {stopwatch.ElapsedMilliseconds} ms.");
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
    }
}
