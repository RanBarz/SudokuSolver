using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static bool IsInvalid(SudokuGrid grid)
        {
            bool hasDuplicates = false;

            hasDuplicates = GridStructureArrayHasDuplicates(grid.GetRows());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetCols());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetSubgrids());
            return hasDuplicates;
        }

        public static bool GridStructureArrayHasDuplicates(SudokuGridStructure[] arr)
        {
            foreach (var structure in arr)
                if (structure.HasDuplicates())
                    return true;
            return false;
        }
    }
}
