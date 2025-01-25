using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    /// <summary>Base class for row, column, and subgrid structures in the Sudoku grid.</summary>
    internal abstract class SudokuGridStructure
    {
        private Cell[] cells;
        private readonly int gridSize;
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
            occupiedSet = new HashSet<int>();
            this.gridSize = gridSize;
            candidatesMap = new Dictionary<int, HashSet<Cell>>();
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
        /// A method which removes a specific value from the occupied set of an object
        /// </summary>
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

            foreach (Cell cell in cells)
                if (cell.ShouldFill())
                {
                    cell.SetValue();
                    SetOccupied();
                    RemoveCandidates();
                    madeProgress = true;
                }
            return madeProgress;
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
        
        /// <summary>
        /// A method which returns the cell with the least candidates in a structure
        /// </summary>
        public Cell GetCellWithLeastCandidates()
        {
            int min = gridSize + 1;
            Cell minCell = null;
            foreach (Cell cell in cells)
            { 
                if (cell.GetCandidates().Length < min && cell.GetCandidates().Length != 0)
                {
                    minCell = cell;
                    min = cell.GetCandidates().Length;
                }
            }
            return minCell;
        }

        /// <summary>
        /// A method which returns true if the structure has cell, that isn't solved and has no candidates.
        /// </summary>
        public bool HasUnsolvableCell()
        {
            foreach (Cell cell in cells)
                if (cell.GetCandidates().Length == 0 && !cell.IsSolved())
                    return true;
            return false;
        }

        /// <summary>
        /// A method which returns true if astructure has duplicate values in it.
        /// </summary>
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

        public bool NakedPair()
        {
            int isPair;
            Cell pair = null;
            bool progressed = false;

            foreach (Cell cell in cells)
            {
                if (cell.GetCandidates().Count() == 2)
                {
                    isPair = 1;
                    foreach (Cell otherCell in cells)
                        if (cell != otherCell && cell.GetCandidates().SequenceEqual(otherCell.GetCandidates()))
                        {
                            isPair += 1;
                            pair = otherCell;
                        }
                    if (isPair == 2)
                        foreach (Cell notPartOfPair in cells)
                        {
                            if (notPartOfPair != cell && notPartOfPair != pair)
                                foreach(int candidate in cell.GetCandidates())
                                    progressed = notPartOfPair.RemoveCandidate(candidate) || progressed;
                        }
                }
            }
            return progressed;
        }

        /// <summary>
        /// A method which removes candidates from a cell, according to the Hidden Single technique
        /// </summary>
        public bool HiddenSingle()
        {
            bool progressed = false;
            SetCandidatesMap();

            foreach (KeyValuePair<int, HashSet<Cell>> kvp in candidatesMap)
            {
                if (kvp.Value.Count == 1 && !kvp.Value.First<Cell>().IsSolved())
                {
                    Cell cell = kvp.Value.First<Cell>();
                    cell.SetCandidates(new HashSet<int>() { kvp.Key });
                    progressed = true;
                }
            }

            return progressed;
        }

        /// <summary>
        /// A method which removes candidates from a cell, according to the Hidden Pair technique
        /// </summary>
        /// <returns></returns>
        public bool HiddenPair()
        {
            bool progressed = false;
            int isPair, otherKey = 0;
            SetCandidatesMap();

            foreach (KeyValuePair<int, HashSet<Cell>> kvp in candidatesMap)
            {
                if (kvp.Value.Count == 2)
                {
                    isPair = 1;
                    foreach (KeyValuePair<int, HashSet<Cell>> otherKvp in candidatesMap)
                    {
                        if (otherKvp.Key != kvp.Key && kvp.Value.SequenceEqual(otherKvp.Value))
                        {
                            isPair++;
                            otherKey = otherKvp.Key;
                        }
                    }
                    if (isPair == 2)
                    {
                        foreach (Cell cell in kvp.Value)
                            cell.SetCandidates(new HashSet<int>() { kvp.Key, otherKey });

                        progressed = true;
                    }
                }
            }

            return progressed;
        }

        /// <summary>
        /// A method which sets the candidatesMap of a structure
        /// </summary>
        private void SetCandidatesMap()
        {
            candidatesMap.Clear();

            foreach (Cell cell in cells)
            {
                    foreach (int candidate in cell.GetCandidates())
                    {
                        if (!candidatesMap.ContainsKey(candidate))
                            candidatesMap[candidate] = new HashSet<Cell>();

                        candidatesMap[candidate].Add(cell);
                    }
            }
        }

        /// <summary>
        /// Returns a string representation of all cells in this structure
        /// </summary>
        public override string ToString()
        {
            string structure = "";
            foreach (Cell cell in cells)
                structure += cell.ToString();
            return structure;
        }
    }
}