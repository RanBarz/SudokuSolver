using System;

namespace SudokuSolver.Core
{
    /// <summary>
    /// Represents a subgrid (3x3 for a 9x9 grid) of the Sudoku grid.
    /// </summary>
    internal class Subgrid :SudokuGridStructure
    {
        public Subgrid(Cell[] cells) : base(cells) { }
    }
}
