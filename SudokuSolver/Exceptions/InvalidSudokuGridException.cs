using System;

namespace SudokuSolver.Core.Exceptions
{
    /// <summary>
    /// An exception indincating the given grid doesn't follow Sudoku rules.
    /// </summary>
    public class InvalidSudokuGridException : Exception
    {
        public InvalidSudokuGridException() { }
        public InvalidSudokuGridException(string message) : base(message) { }
    }
}
