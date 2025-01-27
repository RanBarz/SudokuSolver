using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core.Exceptions
{
    internal class IllegalFileOfSudokuGridException :Exception
    {
        public IllegalFileOfSudokuGridException() { }
        public IllegalFileOfSudokuGridException(string message) : base(message) { }
    }
}
