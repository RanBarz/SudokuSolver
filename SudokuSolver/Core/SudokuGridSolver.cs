using SudokuSolver.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace SudokuSolver.Core
{
    /// <summary>
    /// The class offering a solving algorithm, 
    /// to a string representation of a Sudoku grid.
    /// </summary>
    internal class SudokuGridSolver
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
            ApplySolvingMethods(rows, cols, subgrids);
            if (grid.IsSolved())
                return grid.ToString();
            throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
        }

        /// <summary>
        /// A method which applies both human solving heuristics and backtracking until the grid is solved,
        /// or throws UnsolvableSudokuException
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="subgrids"></param>
        /// <exception cref="UnsolvableSudokuGridException"></exception>
        private void ApplySolvingMethods(SudokuGridStructure[] rows, SudokuGridStructure[] cols, SudokuGridStructure[] subgrids)
        {
            bool progressed = true;
            int index = 0;
            while (!grid.IsSolved() && progressed)
            {
                progressed = RemoveCandidatesSudokuGridStructureArray(rows);
                progressed = RemoveCandidatesSudokuGridStructureArray(cols) || progressed;
                progressed = RemoveCandidatesSudokuGridStructureArray(subgrids) || progressed;
                progressed = SingleCandidateSudokuGridStructureArray(rows) || progressed;
                progressed = SingleCandidateSudokuGridStructureArray(cols) || progressed;
                progressed = SingleCandidateSudokuGridStructureArray(subgrids) || progressed;
                if (SudokuGridValidator.IsUnsolvable(grid) || SudokuGridValidator.IsInvalid(grid))
                    throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
                if (!progressed && !grid.IsSolved())
                    Backtrack();
                index++;
            }
        }

        /// <summary>
        /// Applies solving strategies to an array of grid structures (rows, columns, or subgrids)
        /// </summary>
        /// <returns>True if any progress was made in solving</returns>
        public static bool RemoveCandidatesSudokuGridStructureArray(SudokuGridStructure[] arr)
        {
            bool progressed = false;
            foreach (SudokuGridStructure structure in arr)
            {
                progressed = structure.RemoveCandidates() || progressed;
                progressed = structure.NakedCombinations() || progressed;
                progressed = structure.HiddenSingle() || progressed;
            }
                return progressed;
        }

        /// <summary>
        /// A method which receives an array of sudoku grid structures and fills cells,
        /// which only have one candidate
        /// </summary>
        /// <returns>True if any progress was made in solving</returns>
        public static bool SingleCandidateSudokuGridStructureArray(SudokuGridStructure[] arr)
        {
            foreach (SudokuGridStructure structure in arr)
                if (structure.SingleCandidate())
                    return true;
            return false;
        }

        /// <summary>
        /// A method which finds a good cell for guessing (least candidates), and starts backtracking. 
        /// </summary>
        public void Backtrack()
        {
            SudokuGridStructure structure = grid.GetMostOccupiedGridStructure();
            Cell cell = structure.GetCellWithLeastCandidates();
            HashSet<int> candidates = new HashSet<int>(cell.GetCandidates());
            RecursiveSolve(cell, candidates, structure);
        }

        /// <summary>
        /// A method which trys solving the grid by "guessing" a specific cell's values.
        /// </summary>
        public void RecursiveSolve(Cell cell, HashSet<int> candidates, SudokuGridStructure structure)
        {
            SudokuGridSolver tryGrid;

            foreach (int candidate in candidates)
            {
                cell.SetValue(candidate);
                structure.SetOccupied();
                tryGrid = new SudokuGridSolver(grid.Copy());
                try
                {
                    grid = new SudokuGrid(tryGrid.Solve(),
                        GetGridSize(grid.ToString()));
                    break;
                }
                catch (UnsolvableSudokuGridException)
                {
                    cell.SetValue(0);
                    structure.RemoveFromOccupied(candidate);
                    cell.SetCandidates(candidates);
                    cell.RemoveCandidate(candidate);
                }
            }
        }
    }
}
