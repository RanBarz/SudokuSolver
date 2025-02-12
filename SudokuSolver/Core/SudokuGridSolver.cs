using SudokuSolver.Core.Entities;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.Core.Helpers;
using System;
using System.Collections.Generic;

namespace SudokuSolver.Core
{
    /// <summary>
    /// The class offering a solving algorithm, 
    /// to a string representation of a Sudoku grid.
    /// </summary>
    public class SudokuGridSolver
    {
        private SudokuGrid grid;

        /// <summary>
        /// Initializes a new solver with a string representation of the grid
        /// </summary>
        /// <param name="grid">String representation of the Sudoku grid</param>
        public SudokuGridSolver(string grid)
        {
            int gridSize = GetGridSize(grid);
            this.grid = new SudokuGrid(grid, gridSize);
            if (SudokuGridValidator.IsInvalid(this.grid))
                throw new InvalidSudokuGridException("This grid contains duplicates in a row / column / subgrid.");
        }

        private SudokuGridSolver(SudokuGrid grid)
        {
            this.grid = grid;
        }

        public static int GetGridSize(string grid)
        {
            return (int) Math.Sqrt(grid.Length);
        }

        /// <summary>
        /// Attempts to solve the Sudoku puzzle using row, column, and subgrid solving strategies
        /// </summary>
        public string Solve()
        {
            SudokuGridStructure[] rows = grid.GetRows();
            SudokuGridStructure[] cols = grid.GetCols();
            SudokuGridStructure[] subgrids = grid.GetSubgrids();


            NakedCombinationsOnArray(rows);
            NakedCombinationsOnArray(cols);
            NakedCombinationsOnArray(subgrids);

            ApplySolvingMethods(rows, cols, subgrids);
            if (grid.IsSolved())
                return grid.ToString();
            throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
        }

        public string SolveBacktrack()
        {
            SudokuGridStructure[] rows = grid.GetRows();
            SudokuGridStructure[] cols = grid.GetCols();
            SudokuGridStructure[] subgrids = grid.GetSubgrids();

            ApplySolvingMethods(rows, cols, subgrids);
            if (grid.IsSolved())
                return grid.ToString();
            throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
        }

        /// <summary>
        /// A method which applies both human solving heuristics and backtracking until the grid is solved,
        /// or throws UnsolvableSudokuException
        /// </summary>
        private void ApplySolvingMethods(SudokuGridStructure[] rows, SudokuGridStructure[] cols, SudokuGridStructure[] subgrids)
        {
            bool progressed = true;


            while (!grid.IsSolved() && progressed)
            {
                progressed = RemoveCandidatesFromArray(rows);
                progressed = RemoveCandidatesFromArray(cols) || progressed;
                progressed = RemoveCandidatesFromArray(subgrids) || progressed;
                if (SudokuGridValidator.IsUnsolvable(grid) || SudokuGridValidator.IsInvalid(grid))
                    throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
                if (!progressed && !grid.IsSolved())
                    Backtrack();
            }
        }

        /// <summary>
        /// Applies solving strategies to an array of grid structures (rows, columns, or subgrids)
        /// </summary>
        /// <returns>True if any progress was made in solving</returns>
        internal static bool RemoveCandidatesFromArray(SudokuGridStructure[] arr)
        {
            bool progressed = false;
            foreach (SudokuGridStructure structure in arr)
            {
                if (!structure.IsSolved())
                {
                    progressed = SudokuHeuristics.NakedSingle(structure) || progressed;
                    progressed = SudokuHeuristics.HiddenSingle(structure) || progressed;
                    progressed = SudokuHeuristics.HiddenPair(structure) || progressed;
                }
            }
            return progressed;
        }

        /// <summary>
        /// Applies solving strategies to an array of grid structures (rows, columns, or subgrids)
        /// </summary>
        /// <returns>True if any progress was made in solving</returns>
        internal static void NakedCombinationsOnArray(SudokuGridStructure[] arr)
        {
            bool progressed = false;
            foreach (SudokuGridStructure structure in arr)
            {
                if (!structure.IsSolved())
                {
                    progressed = SudokuHeuristics.NakedSingle(structure) || progressed;
                    progressed = SudokuHeuristics.NakedCombinations(structure) || progressed;
                    progressed = SudokuHeuristics.HiddenSingle(structure) || progressed;
                }
            }
        }

        /// <summary>
        /// A method which finds a good cell for guessing (least candidates), and starts backtracking. 
        /// </summary>
        public void Backtrack()
        {
            Cell cell = grid.GetCellWithLeastCandidates();
            var candidates = new HashSet<int>(cell.GetCandidates());
            RecursiveSolve(cell, candidates);
        }

        /// <summary>
        /// A method which trys solving the grid by "guessing" a specific cell's values.
        /// </summary>
        internal void RecursiveSolve(Cell cell, HashSet<int> candidates)
        {
            SudokuGridSolver tryGrid;

            foreach (int candidate in candidates)
            {
                cell.SetValue(candidate);
                tryGrid = new SudokuGridSolver(grid.Copy());
                try
                {
                    grid = new SudokuGrid(tryGrid.SolveBacktrack(),
                        GetGridSize(grid.ToString()));
                    break;
                }
                catch (UnsolvableSudokuGridException)
                {
                    cell.SetValue(0);
                    cell.SetCandidates(candidates);
                    cell.RemoveCandidate(candidate);
                }
            }
        }
    }
}
