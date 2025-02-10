using SudokuSolver.Core.Exceptions;
using System;
using System.IO;

namespace SudokuSolver.Core.Helpers
{
    /// <summary>
    /// The class offering validation to both string representation of sudoku grids,
    /// and objects of them.
    /// </summary>
    public static class SudokuGridValidator
    {
        /// <summary>
        /// Returns true if an unsolved cell has no candidates.
        /// </summary>
        internal static bool IsUnsolvable(SudokuGrid grid)
        {
            foreach (SudokuGridStructure row in grid.GetRows())
                if (row.HasUnsolvableCell())
                    return true;
            return false;
        }

        /// <summary>
        /// Returns true if the grid has duplicates in one of its structures.
        /// </summary>
        internal static bool IsInvalid(SudokuGrid grid)
        {
            bool hasDuplicates;

            hasDuplicates = GridStructureArrayHasDuplicates(grid.GetRows());
            hasDuplicates = hasDuplicates ||
                GridStructureArrayHasDuplicates(grid.GetCols());
            hasDuplicates = hasDuplicates ||
                GridStructureArrayHasDuplicates(grid.GetSubgrids());
            return hasDuplicates;
        }

        /// <summary>
        /// A method which receives an array of grid structures. and returns true if one of them has a
        /// duplicate value.
        /// </summary>
        internal static bool GridStructureArrayHasDuplicates(SudokuGridStructure[] arr)
        {
            foreach (SudokuGridStructure structure in arr)
                if (structure.HasDuplicates())
                    return true;
            return false;
        }
    }
}
