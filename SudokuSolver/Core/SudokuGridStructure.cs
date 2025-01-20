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
        private Dictionary<int, HashSet<Cell>> candidatesMap;

        /// <summary>
        /// Initializes a grid structure with an array of cells and specified grid size
        /// </summary>
        /// <param name="cells">Array of cells in this structure</param>
        /// <param name="gridSize">Size of the Sudoku grid</param>
        public SudokuGridStructure(Cell[] cells, int gridSize)
        {
            this.cells = cells;
            this.occupiedSet = new HashSet<int>();
            this.candidatesMap = new Dictionary<int, HashSet<Cell>>(gridSize);
            SetOccupied();
        }

        /// <summary>
        /// Returns the cell at the specified index in this structure
        /// </summary>
        /// <param name="index">Index of the cell to retrieve</param>
        /// <returns>The cell at the specified index</returns>
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

        /// <summary>
        /// Returns the number of cells that have been assigned values in this structure
        /// </summary>
        public int GetOccupiedCount() => occupiedSet.Count;

        /// <summary>
        /// Checks each cell for single remaining candidate and fills if found
        /// </summary>
        /// <returns>True if any cell was filled</returns>
        public bool SingleCandidate()
        {
            bool madeProgress = false;
            SetOccupied();

            foreach (var cell in cells)
                if (cell.ShouldFill())
                {
                    cell.SetValue();
                    SetOccupied();
                    RemoveCandidates();
                    madeProgress = true;
                }
            if (!madeProgress) return false;
            return true;
        }

        /// <summary>
        /// Identifies and fills cells that are the only possible location for a number in this structure
        /// </summary>
        /// <returns>True if any cell was filled</returns>
        public bool HiddenSingle()
        {
            bool progressed = false;

            SetCandidatesMap();
            foreach (KeyValuePair<int, HashSet<Cell>> pair in candidatesMap)
                if (pair.Value.Count == 1)
                {
                    pair.Value.ElementAt(0).SetValue(pair.Key);
                    SetOccupied();
                    progressed = true;
                }

            if (!progressed) return false;
            return true;
        }

        /// <summary>
        /// Removes invalid candidates based on current cell values.
        /// </summary>
        public bool RemoveCandidates()
        {
            HashSet<Cell> removedFrom = new HashSet<Cell>();
            foreach (Cell cell in cells)
                foreach (int occupied in occupiedSet)
                    if (cell.RemoveCandidate(occupied) && !cell.IsSolved())
                        removedFrom.Add(cell);
            SetOccupied();
            return removedFrom.Count > 0;
        }

        /// <summary>
        /// Updates the mapping of candidate numbers to cells that could contain them
        /// </summary>
        public void SetCandidatesMap()
        {
            foreach (var cell in cells)
                foreach (int candidate in cell.GetCandidates())
                {
                    if (!candidatesMap.TryGetValue(candidate, out HashSet<Cell> cellsOfCandidate))
                        candidatesMap.Add(candidate, new HashSet<Cell>());
                    candidatesMap.TryGetValue(candidate, out cellsOfCandidate);
                    cellsOfCandidate.Add(cell);
                }
            List<Cell> toRemove = new List<Cell>();
            foreach (KeyValuePair<int, HashSet<Cell>> pair in candidatesMap)
            {
                toRemove.Clear();
                foreach (var cell in pair.Value)
                    if (!cell.GetCandidates().Contains(pair.Key))
                        toRemove.Add(cell);
                foreach (var cell in toRemove)
                    pair.Value.Remove(cell);
            }
        }

        /// <summary>
        /// Compares structures based on number of occupied cells.
        /// </summary>
        public int CompareTo(SudokuGridStructure other)
        {
            if (other == null)
                throw new ArgumentNullException("other");
            return this.occupiedSet.Count - other.occupiedSet.Count;
        }

        /// <summary>
        /// Returns a string representation of all cells in this structure
        /// </summary>
        public override string ToString()
        {
            string structure = "";
            foreach (var cell in cells)
                structure += cell.ToString();
            return structure;
        }
    }
}