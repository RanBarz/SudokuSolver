using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    /// <summary>Base class for row, column, and subgrid structures in the Sudoku grid.</summary>
    internal abstract class SudokuGridStructure : IComparable<SudokuGridStructure>
    {
        private Cell[] cells;
        private int gridSize;
        private HashSet<int> occupiedSet;
        private Dictionary<int, List<Cell>> candidatesMap;

        /// <summary>
        /// Initializes a grid structure with an array of cells and specified grid size
        /// </summary>
        /// <param name="cells">Array of cells in this structure</param>
        /// <param name="gridSize">Size of the Sudoku grid</param>
        public SudokuGridStructure(Cell[] cells, int gridSize)
        {
            this.cells = cells;
            this.occupiedSet = new HashSet<int>();
            this.gridSize = gridSize;
            this.candidatesMap = new Dictionary<int, List<Cell>>();
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

        public void RemoveFromOccupied(int value)
        {
            occupiedSet.Remove(value);
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
            RemoveCandidates();
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
        /// Removes invalid candidates based on current cell values.
        /// </summary>
        public bool RemoveCandidates()
        {
            HashSet<Cell> removedFrom = new HashSet<Cell>();
            SetOccupied();
            foreach (Cell cell in cells)
                foreach (int occupied in occupiedSet)
                    if (cell.RemoveCandidate(occupied) && !cell.IsSolved())
                        removedFrom.Add(cell);
            return removedFrom.Count > 0;
        }
        
        public Cell GetCellWithLeastCandidates()
        {
            int min = gridSize + 1;
            Cell minCell = null;
            foreach (var cell in cells)
            { 
                if (cell.GetCandidates().Length < min && cell.GetCandidates().Length != 0)
                {
                    minCell = cell;
                    min = cell.GetCandidates().Length;
                }
            }
            return minCell;
        }

        public bool HasUnsolvableCell()
        {
            foreach (var cell in cells)
                if (cell.GetCandidates().Length == 0 && !cell.IsSolved())
                    return true;
            return false;
        }

        public bool HasDuplicates()
        {
            bool [] flagArr = new bool[gridSize + 1];

            for (int i = 0; i < gridSize; i++)
                if (cells[i].GetValue() != 0 &&
                    flagArr[cells[i].GetValue()])
                    return true;
                else
                    flagArr[cells[i].GetValue()] = true;
            return false;
        }

        public bool HiddenSingle()
        {
            bool progressed = false;
            SetCandidatesMap();

            foreach (var kvp in candidatesMap)
            {
                if (kvp.Value.Count == 1 && !kvp.Value[0].IsSolved())
                {
                    var cell = kvp.Value[0];
                    cell.SetCandidates(new HashSet<int>() { kvp.Key });
                    progressed = true;
                }
            }

            return progressed;
        }

        private void SetCandidatesMap()
        {
            candidatesMap.Clear();

            foreach (var cell in cells)
            {
                    foreach (int candidate in cell.GetCandidates())
                    {
                        if (!candidatesMap.ContainsKey(candidate))
                            candidatesMap[candidate] = new List<Cell>();

                        candidatesMap[candidate].Add(cell);
                    }
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