using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Exceptions
{
    internal class IllegalStringOfSudokuGridException: Exception
    {
        public IllegalStringOfSudokuGridException() { }
        public IllegalStringOfSudokuGridException(string message) : base(message) { }
    }
}
