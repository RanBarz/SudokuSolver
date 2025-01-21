using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    internal class SudokuGridValidator
    {
        public static bool IsUnsolvable(SudokuGrid grid)
        {
            foreach (var row in grid.GetRows())
                if (row.HasUnsolvableCell())
                    return true;
            return false;
        }
    }
}
