using System;

namespace SudokuSolver.Core
{
    /// <summary>
    /// Represents a vertical column in the Sudoku grid.
    /// </summary>
    internal class Column : SudokuGridStructure
    {
        public Column(Cell[] cells, int gridSize) : base(cells, gridSize) { }
    }
}
