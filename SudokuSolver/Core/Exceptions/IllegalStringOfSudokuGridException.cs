using System;

namespace SudokuSolver.Core.Exceptions
{
    /// <summary>
    /// An exception for when a string doesn't follow the rules of representing a sudoku grid
    /// </summary>
    internal class IllegalStringOfSudokuGridException: Exception
    {
        public IllegalStringOfSudokuGridException() { }
        public IllegalStringOfSudokuGridException(string message) : base(message) { }
    }
}
