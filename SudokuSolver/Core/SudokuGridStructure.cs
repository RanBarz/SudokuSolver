using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    /// <summary>Base class for row, column, and subgrid structures in the Sudoku grid.</summary>
    internal abstract class SudokuGridStructure : IComparable<SudokuGridStructure>
    {
        private Cell[] cells;
        private HashSet<int> occupiedSet;

        public SudokuGridStructure(Cell[] cells)
        {
            this.cells = cells;
            this.occupiedSet = new HashSet<int>();
            SetOccupied();
            
        }

        public Cell Get(int index) => cells[index];

        /// <summary>
        /// Counts and sets the number of cells with non-zero values.
        /// </summary>
        public void SetOccupied()
        {
            foreach (Cell cell in cells) 
                if (cell.GetValue() != 0 &&
                    !occupiedSet.Contains(cell.GetValue()))
                    occupiedSet.Add(cell.GetValue());
        }

        public int GetOccupiedCount() => occupiedSet.Count;

        public bool Solve()
        {
            bool madeProgress = false;
            var removedFrom = RemoveCandidates();
            foreach (var cell in removedFrom)
                if (cell.ShouldFill())
                {
                    cell.SetValue();
                    SetOccupied();
                    madeProgress = true;
                }
            if (!madeProgress && removedFrom.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// Removes invalid candidates based on current cell values.
        /// </summary>
        public HashSet<Cell> RemoveCandidates()
        {
            HashSet<Cell> removedFrom = new HashSet<Cell>();
            foreach (Cell cell in cells)
                foreach (int occupied in occupiedSet)
                    if (cell.RemoveCandidate(occupied) && !cell.IsSolved())
                        removedFrom.Add(cell);
            return removedFrom;
        }

        public bool OnlyCellThatHasIt(Cell cell, int candidate)
        {
            foreach (var otherCell in cells)
            {
                if (otherCell.HasCandidate(candidate) && otherCell != cell)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Compares structures based on number of occupied cells.
        /// </summary>
        public int CompareTo(SudokuGridStructure other)
        {
            if (other == null)
                throw new ArgumentNullException("other");
            return this.occupiedSet.Count- other.occupiedSet.Count;
        }

        public override string ToString()
        {
            string structure = "";
            foreach (var cell in cells)
                structure += cell.ToString();
            return structure;
        }
    }
}
