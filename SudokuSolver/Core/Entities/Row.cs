
namespace SudokuSolver.Core
{
    /// <summary>
    /// Represents a horizontal row in the Sudoku grid.
    /// </summary>
    internal class Row : SudokuGridStructure
    {
        public Row(Cell[] cells, int gridSize) : base(cells, gridSize) { }
    }
}
