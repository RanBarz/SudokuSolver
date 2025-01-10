using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Exceptions
{
    /// <summary>
    /// An exception indincating the given grid doesn't follow Sudoku rules.
    /// </summary>
    internal class InvalidSudokuGridException : Exception
    {
        public InvalidSudokuGridException() { }
        public InvalidSudokuGridException(string message) : base(message) { }
    }
}
