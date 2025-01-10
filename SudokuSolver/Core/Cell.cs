using System;
using System.Collections.Generic;


namespace SudokuSolver.Core
{
    /// <summary>Represents a single cell in a Sudoku grid,
    /// managing its value and candidates.</summary>
    internal class Cell
    {
        private List<int> candidates;
        private int value;
        private int gridSize;

        /// <summary>Initializes a new cell with a value
        /// and possible candidates.</summary>
        public Cell(int value, int gridSize)
        {
            this.gridSize = gridSize;
            this.value = value;
            this.candidates = new List<int>();
            setCandidates();
        }

        /// <summary>Initializes the list of possible candidates
        /// for this cell.</summary>
        private void setCandidates()
        {
            for (int i = 1; i <= gridSize; i++)
                candidates.Add(i);
        }

        public int GetValue() => value;

        public void SetValue(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// Removes a number from the list of possible candidates.
        /// </summary>
        public void RemoveCandidate(int candidate)
        {
            candidates.Remove(candidate);
        }

        public int[] GetCandidates() => candidates.ToArray();
    }
}
