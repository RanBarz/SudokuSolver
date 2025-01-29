using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    internal static class SudokuHeuristics
    {
        private const int MaxForNakedCombinations = 9;
        private const int NakedSizeForLargeGrids = 2;

        /// <summary>
        /// Runs the naked heuristic on a SudokuGridStructure, fro pairs, triples and so on.
        /// </summary>
        public static bool NakedCombinations(SudokuGridStructure structure)
        {
            Cell[] cells = structure.GetCells();
            List<HashSet<Cell>> allCombinations = new List<HashSet<Cell>>();
            bool progressed = false;
            int gridSize = structure.GetGridSize(), 
                nakedSize = gridSize > MaxForNakedCombinations ? NakedSizeForLargeGrids : gridSize;

            for (int combinationSize = 2; combinationSize < Math.Min(gridSize - 1, nakedSize); combinationSize++)
            {
                allCombinations.Clear();
                allCombinations = GetAllCombinations(cells, combinationSize);
                HandleNakedCombinations(cells, allCombinations, ref progressed);
            }
            return progressed;
        }

        /// <summary>
        /// This function receives all the combinations of cells, and removes candidates according to the heuristic.
        /// </summary>
        private static void HandleNakedCombinations(Cell[] cells, List<HashSet<Cell>> cellsOfCombination, 
            ref bool progressed)
        {
            HashSet<int> allCandidates;
            foreach (HashSet<Cell> combination in cellsOfCombination)
            {
                allCandidates = GetAllCandidates(combination);
                if (allCandidates.Count == combination.Count)
                {
                    foreach (Cell cell in cells)
                    {
                        if (!combination.Contains(cell))
                        {
                            foreach (int candidate in allCandidates)
                                progressed = cell.RemoveCandidate(candidate) || progressed;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Receives cells and return a set of their candidates.
        /// </summary>
        private static HashSet<int> GetAllCandidates(HashSet<Cell> cells)
        {
            HashSet<int> allCandidates = new HashSet<int>();
            foreach (Cell cell in cells)
            {
                allCandidates.UnionWith(cell.GetCandidates());
            }
            return allCandidates;
        }

        /// <summary>
        /// Returns all the combinations of a certain size, in an array of cells.
        /// </summary>
        private static List<HashSet<Cell>> GetAllCombinations(Cell[] cells, int combinationSize)
        {
            List<HashSet<Cell>> allCombinations = new List<HashSet<Cell>>();

            if (combinationSize == 0)
            {
                allCombinations.Add(new HashSet<Cell>());
                return allCombinations;
            }

            for (int i = 0; i < cells.Length - combinationSize; i++)
            {
                if (!cells[i].IsSolved())
                {
                    List<HashSet<Cell>> leftCombinations =
                        GetAllCombinations(cells.Skip(i + 1).ToArray(), combinationSize - 1);
                    foreach (HashSet<Cell> combination in leftCombinations)
                    {
                        HashSet<Cell> outcome = new HashSet<Cell>() { cells[i] };
                        outcome.UnionWith(combination);
                        allCombinations.Add(outcome);
                    }
                }
            }
            return allCombinations;
        }

        /// <summary>
        /// A method which removes candidates from a cell, according to the Hidden Single technique
        /// </summary>
        public static bool HiddenSingle(SudokuGridStructure structure)
        {
            bool progressed = false;
            structure.SetCandidatesMap();

            foreach (KeyValuePair<int, HashSet<Cell>> kvp in structure.GetCandidatesMap())
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
        /// Checks each cell for single remaining candidate and fills if found
        /// </summary>
        /// <returns>True if any cell was filled</returns>
        public static bool NakedSingle(SudokuGridStructure structure)
        {
            bool madeProgress = false;
            RemoveCandidates(structure);

            foreach (Cell cell in structure.GetCells())
                if (cell.ShouldFill())
                {
                    cell.SetValue();
                    RemoveCandidates(structure);
                    madeProgress = true;
                }
            return madeProgress;
        }

        /// <summary>
        /// Removes invalid candidates based on current cell values.
        /// </summary>
        public static bool RemoveCandidates(SudokuGridStructure structure)
        {
            HashSet<Cell> removedFrom = new HashSet<Cell>();
            structure.SetOccupied();

            foreach (Cell cell in structure.GetCells())
                foreach (int occupied in structure.GetOccupiedSet())
                    if (cell.RemoveCandidate(occupied) && !cell.IsSolved())
                        removedFrom.Add(cell);
            return removedFrom.Count > 0;
        }
    }
}
