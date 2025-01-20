using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;


namespace SudokuSolver.Core
{
    /// <summary>Represents a single cell in a Sudoku grid,
    /// managing its value and candidates.</summary>
    internal class Cell
    {
        private HashSet<int> candidatesSet;
        private int value;
        private int gridSize;

        /// <summary>Initializes a new cell with a value
        /// and possible candidates.</summary>
        public Cell(int value, int gridSize)
        {
            this.gridSize = gridSize;
            this.value = value;
            this.candidatesSet = new HashSet<int>();
            SetCandidates();
        }

        /// <summary>Initializes the list of possible candidates
        /// for this cell.</summary>
        private void SetCandidates()
        {
            if (!IsSolved())
                for (int i = 1; i <= gridSize; i++)
                    candidatesSet.Add(i);
        }

        /// <summary>
        /// Sets the candidates for this cell to a specific set of values
        /// </summary>
        public void SetCandidates(HashSet<int> candidates)
        {
            this.candidatesSet = new HashSet<int>(candidates);
        }

        /// <summary>
        /// Returns the current value of the cell
        /// </summary>
        public int GetValue() => value;

        /// <summary>
        /// Sets the cell's value to its only remaining candidate
        /// </summary>
        public void SetValue()
        {
            this.value = candidatesSet.ElementAt<int>(0);
            candidatesSet.Clear();
        }

        /// <summary>
        /// Sets the cell's value to a specific number
        /// </summary>
        public void SetValue(int value)
        {
            this.value = value;
            candidatesSet.Clear();
        }

        /// <summary>
        /// Removes a number from the list of possible candidates.
        /// </summary>
        public bool RemoveCandidate(int candidate)
        {
            return candidatesSet.Remove(candidate);
        }

        /// <summary>
        /// Checks if the cell has only one remaining candidate
        /// </summary>
        public bool ShouldFill() => candidatesSet.Count == 1;

        /// <summary>
        /// Returns an array of all current candidate values
        /// </summary>
        public int[] GetCandidates() => candidatesSet.ToArray<int>();

        /// <summary>
        /// Checks if a specific number is a candidate for this cell
        /// </summary>
        public bool HasCandidate(int candidate)
            => candidatesSet.Contains(candidate);

        /// <summary>
        /// Returns the string representation of the cell's value
        /// </summary>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        /// Checks if the cell has been solved (has a non-zero value)
        /// </summary>
        public bool IsSolved() => value != 0;
    }
}
