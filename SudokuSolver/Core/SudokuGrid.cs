using System;

namespace SudokuSolver.Core
{
    /// <summary>
    /// Represents the main Sudoku grid and manages solving operations.
    /// </summary>
    internal class SudokuGrid
    {
        private int gridSize;
        private int occupiedCellsCount;
        private SudokuGridStructure[] rows, cols, subgrids;
        private const int GridSize = 9;

        /// <summary>
        /// Initializes a new Sudoku grid from a string representation.
        /// </summary>
        public SudokuGrid(string grid)
        {
            this.gridSize = GridSize;
            Cell[] cells = new Cell[gridSize * gridSize];
            SetCells(grid, cells);
            SetRows(cells);
            SetCols(cells);
            SetSubgrids(cells);
        }

        /// <summary>
        /// Solves the Sudoku puzzle using constraint propagation, 
        /// and backtracking.
        /// </summary>
        public void Solve()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the cells array from the input string.
        /// </summary>
        public void SetCells(string grid, Cell[] cells)
        {
            for (int i = 0; i < grid.Length; i++)
                cells[i] = new Cell(grid[i], gridSize);
        }

        /// <summary>
        /// Organizes cells into rows for processing.
        /// </summary>
        public void SetRows(Cell[] cells)
        {
            Cell[] cellsOfRow = new Cell[gridSize];
            rows = new Row[gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j  = 0; j < gridSize; j++)
                    cellsOfRow[j] = cells[j + i * gridSize];
                rows[i] = new Row((Cell[])cellsOfRow.Clone());
            }
        }

        /// <summary>
        /// Organizes cells into columns for processing.
        /// </summary>
        public void SetCols(Cell[] cells) 
        {
            Cell[] cellsOfCol = new Cell[gridSize];
            cols = new Column[gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                    cellsOfCol[j] = cells[i + j * gridSize];
                cols[i] = new Column((Cell[])cellsOfCol.Clone());
            }
        }

        /// <summary>
        /// Organizes cells into subgrids for processing.
        /// </summary>
        public void SetSubgrids(Cell[] cells)
        {
            int subgridSize = (int)Math.Sqrt(gridSize);
            Cell[] cellsOfSub = new Cell[gridSize];
            subgrids = new Subgrid[gridSize];
            for (int i = 0; i < subgridSize; i++)
            {
                for (int j = 0; j < subgridSize; j++)
                {
                    for (int k = 0; k < subgridSize; k++)
                        for (int l = 0; l < subgridSize; l++)
                            cellsOfSub[l + k * subgridSize] =
                                cells[i * subgridSize * gridSize +
                                j * subgridSize + k * gridSize + l];
                    subgrids[j + i * subgridSize] =
                        new Subgrid((Cell[])cellsOfSub.Clone());
                }
            }
        }
    }
}
