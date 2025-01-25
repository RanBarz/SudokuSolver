using System;

namespace SudokuSolver.Core
{
    /// <summary>
    /// Represents the main Sudoku grid and manages solving operations.
    /// </summary>
    internal class SudokuGrid
    {
        private readonly int gridSize;
        private int occupiedCellsCount;
        private SudokuGridStructure[] rows, cols, subgrids;

        /// <summary>
        /// Initializes a new Sudoku grid from a string representation.
        /// </summary>
        public SudokuGrid(string grid, int gridSize)
        {
            this.gridSize = gridSize;
            Cell[] cells = new Cell[gridSize * gridSize];
            SetCells(grid, cells);
            SetRows(cells);
            SetCols(cells);
            SetSubgrids(cells);
        }

        /// <summary>
        /// Checks if the Sudoku grid is completely solved
        /// </summary>
        public bool IsSolved()
        {
            SetOccupiedCellsCount();
            return occupiedCellsCount == gridSize * gridSize;
        }

        /// <summary>
        /// Updates the count of cells that have been assigned values
        /// </summary>
        public void SetOccupiedCellsCount()
        {
            occupiedCellsCount = 0;
            foreach (SudokuGridStructure row in rows)
            {
                occupiedCellsCount += row.GetOccupiedCount();
            }
        }

        /// <summary>
        /// Initializes the cells array from the input string.
        /// </summary>
        public void SetCells(string grid, Cell[] cells)
        {
            for (int i = 0; i < grid.Length; i++)
                cells[i] = new Cell(grid[i] - '0', gridSize);
        }
        
        public SudokuGridStructure[] GetRows() => rows;

        public SudokuGridStructure[] GetCols() => cols;

        public SudokuGridStructure[] GetSubgrids() => subgrids;

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
                rows[i] = new Row((Cell[])cellsOfRow.Clone(), gridSize);
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
                cols[i] = new Column((Cell[])cellsOfCol.Clone(), gridSize);
            }
        }

        /// <summary>
        /// Organizes cells into subgrids for processing.
        /// </summary>
        public void SetSubgrids(Cell[] cells)
        {
            int subgridSize = (int)Math.Sqrt(gridSize);
            subgrids = new Subgrid[gridSize];

            for (int subgridRow = 0; subgridRow < subgridSize; subgridRow++)
            {
                for (int subgridCol = 0; subgridCol < subgridSize; subgridCol++)
                {
                    Cell[] cellsInCurrentSubgrid = ExtractCellsForSubgrid(cells, subgridRow, subgridCol, subgridSize);
                    int subgridIndex = CalculateSubgridIndex(subgridRow, subgridCol, subgridSize);
                    subgrids[subgridIndex] = new Subgrid((Cell[])cellsInCurrentSubgrid.Clone(), gridSize);
                }
            }
        }

        /// <summary>
        /// Extracts cells that belong to a specific subgrid
        /// </summary>
        private Cell[] ExtractCellsForSubgrid(Cell[] cells, int subgridRow, int subgridCol, int subgridSize)
        {
            Cell[] cellsInSubgrid = new Cell[gridSize];

            for (int innerRow = 0; innerRow < subgridSize; innerRow++)
            {
                for (int innerCol = 0; innerCol < subgridSize; innerCol++)
                {
                    int cellIndexInSubgrid = CalculateCellIndexInSubgrid(innerRow, innerCol, subgridSize);
                    int cellIndexInGrid = CalculateCellIndexInGrid(subgridRow, subgridCol, innerRow, innerCol, subgridSize);

                    cellsInSubgrid[cellIndexInSubgrid] = cells[cellIndexInGrid];
                }
            }

            return cellsInSubgrid;
        }

        /// <summary>
        /// Calculates the index of a cell within its subgrid
        /// </summary>
        private int CalculateCellIndexInSubgrid(int innerRow, int innerCol, int subgridSize)
        {
            return innerCol + (innerRow * subgridSize);
        }

        /// <summary>
        /// Calculates the index of a cell in the main grid based on its position in a subgrid
        /// </summary>
        private int CalculateCellIndexInGrid(int subgridRow, int subgridCol, int innerRow, int innerCol, int subgridSize)
        {
            return (subgridRow * subgridSize * gridSize) + // Move to correct row of subgrids
                   (subgridCol * subgridSize) +            // Move to correct column of subgrids
                   (innerRow * gridSize) +                 // Move to correct row within subgrid
                   innerCol;                               // Move to correct column within subgrid
        }

        /// <summary>
        /// Calculates the index of a subgrid in the array of subgrids
        /// </summary>
        private int CalculateSubgridIndex(int subgridRow, int subgridCol, int subgridSize)
        {
            return subgridCol + (subgridRow * subgridSize);
        }

        /// <summary>
        /// A method which returns a SudokuGrid object, which is the copy of the current object.
        /// </summary>
        public SudokuGrid Copy()
        {
            SudokuGrid copy = new SudokuGrid(ToString(), gridSize);
            copy.SetAllOccupied();
            return copy;
        }

        /// <summary>
        /// A method which sets all of a SudokuGrid's structures' occupied sets
        /// </summary>
        public void SetAllOccupied()
        {
            SetSudokuStructuresArrayOccupied(rows);
            SetSudokuStructuresArrayOccupied(cols);
            SetSudokuStructuresArrayOccupied(subgrids);
        }

        /// <summary>
        /// A method which sets the occupied set each structure in an array of SudokuGridStructures
        /// </summary>
        private static void SetSudokuStructuresArrayOccupied(SudokuGridStructure[] arr)
        {
            foreach (SudokuGridStructure structure in arr)
                structure.SetOccupied();
        }

        /// <summary>
        /// Converts the grid to its string representation
        /// </summary>
        public override string ToString()
        {
            string grid = "";
            foreach (SudokuGridStructure row in rows)
                grid += row.ToString();
            return grid;
        }

        /// <summary>
        /// A method which returns the most occupied grid structure in a grid, which isn't fully solved.
        /// </summary>
        public SudokuGridStructure GetMostOccupiedGridStructure()
        {
            SudokuGridStructure maxRow, maxCol, maxSub;
            maxRow = GetMostOccupiedInStructureArray(rows);
            maxCol = GetMostOccupiedInStructureArray(cols);
            maxSub = GetMostOccupiedInStructureArray(subgrids);

            maxRow = maxRow.GetOccupiedCount() > maxCol.GetOccupiedCount() ?
                maxRow : maxCol;
            maxRow = maxRow.GetOccupiedCount() > maxSub.GetOccupiedCount() ?
                maxRow : maxSub;
            return maxRow;
        }

        /// <summary>
        ///  A method which receives an array of grid structures, and returns the most occupied of them.
        ///  *Which isn't fully solved.
        /// </summary>
        public SudokuGridStructure GetMostOccupiedInStructureArray(SudokuGridStructure[] arr)
        {
            int maxOccupied = -1;
            SudokuGridStructure maxStructure = null;

            foreach (SudokuGridStructure structure in arr)
            {
                structure.RemoveCandidates();
                if (structure.GetOccupiedCount() > maxOccupied &&
                    structure.GetOccupiedCount() != gridSize)
                {
                    maxOccupied = structure.GetOccupiedCount();
                    maxStructure = structure;
                }
            }

            return maxStructure;
        }
}
}
