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
            setCandidates();
        }

        /// <summary>Initializes the list of possible candidates
        /// for this cell.</summary>
        private void setCandidates()
        {
            for (int i = 1; i <= gridSize; i++)
                candidatesSet.Add(i);
        }

        public int GetValue() => value;

        public void SetValue()
        {
            this.value = candidatesSet.ElementAt<int>(0);
            candidatesSet.Clear();
        }

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

        public bool ShouldFill() => candidatesSet.Count == 1;

        public int[] GetCandidates() => candidatesSet.ToArray<int>();

        public bool HasCandidate(int candidate) 
            => candidatesSet.Contains(candidate);

        public override string ToString()
        {
            return value.ToString();
        }

        public bool IsSolved() => value != 0;
    }
}
