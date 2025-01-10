using System;

namespace SudokuSolver.Core
{
    /// <summary>Base class for row, column, and subgrid structures in the Sudoku grid.</summary>
    internal abstract class SudokuGridStructure : IComparable<SudokuGridStructure>
    {
        private Cell[] cells;
        private int occupiedCount;

        public SudokuGridStructure(Cell[] cells)
        {
            this.cells = cells;
            SetOccupiedCount();
        }

        public Cell Get(int index) => cells[index];

        /// <summary>
        /// Counts and sets the number of cells with non-zero values.
        /// </summary>
        private void SetOccupiedCount()
        {
            occupiedCount = 0;
            foreach (Cell cell in cells) 
                if (cell.GetValue() != 0)
                    occupiedCount++;
        }

        /// <summary>
        /// Removes invalid candidates based on current cell values.
        /// </summary>
        public bool RemoveCandidates()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compares structures based on number of occupied cells.
        /// </summary>
        public int CompareTo(SudokuGridStructure other)
        {
            if (other == null)
                throw new ArgumentNullException("other");
            return this.occupiedCount - other.occupiedCount;
        }
    }
}
