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

        public static bool LegalStringOfGrid(string grid, int gridSize)
        {
            if (!HasProperSize(grid, gridSize))
                return false;
            if (!HasProperChars(grid, gridSize))
                return false;
            return true;
        }

        public static bool HasProperSize(string grid, int gridSize)
        {
            return grid.Length == gridSize * gridSize;
        }

        public static bool HasProperChars(string grid, int gridSize)
        {
            int value;

            foreach (char c in grid)
            {
                value = c - '0';
                if (value < 0 || value > gridSize)
                    return false;
            }

            return true;
        }
    }
}
