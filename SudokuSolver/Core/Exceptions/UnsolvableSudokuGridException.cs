using System;

namespace SudokuSolver.Core.Exceptions
{
    /// <summary>
    /// An exception indicating the given Sudoku grid can't be solved.
    /// </summary>
    internal class UnsolvableSudokuGridException : Exception
    {
        public UnsolvableSudokuGridException() { }
        public UnsolvableSudokuGridException(string message) : base(message) { }
    }
}
