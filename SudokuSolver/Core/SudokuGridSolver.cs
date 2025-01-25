using SudokuSolver.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static int GetGridSize(string grid)
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
            bool progressed = true;

            while (!grid.IsSolved() && progressed)
            {
                progressed = false;
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
            }
            if (!grid.IsSolved())
                throw new UnsolvableSudokuGridException("The grid you entered is unsolvable.");
            return grid.ToString();
        }

        /// <summary>
        /// Applies solving strategies to an array of grid structures (rows, columns, or subgrids)
        /// </summary>
        /// <param name="arr">Array of grid structures to solve</param>
        /// <returns>True if any progress was made in solving</returns>
        public static bool RemoveCandidatesSudokuGridStructureArray(SudokuGridStructure[] arr)
        {
            bool progressed = false;
            foreach (var structure in arr)
            {
                progressed = structure.RemoveCandidates() || progressed;
                progressed = structure.HiddenSingle() || progressed;
            }
                return progressed;
        }

        public static bool SingleCandidateSudokuGridStructureArray(SudokuGridStructure[] arr)
        {
            foreach (var structure in arr)
                if (structure.SingleCandidate())
                    return true;
            return false;
        }

        public void Backtrack()
        {
            SudokuGridStructure structure = grid.GetMostOccupiedGridStructure();
            Cell cell = structure.GetCellWithLeastCandidates();
            HashSet<int> candidates = new HashSet<int>(cell.GetCandidates());
            SudokuGridSolver tryGrid;
            foreach (var candidate in candidates)
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
