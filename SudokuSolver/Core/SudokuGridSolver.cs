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
            this.grid = new SudokuGrid(grid);
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
                progressed = progressed ? true : SolveSudokuGridStructureArray(rows);
                progressed = progressed ? true : SolveSudokuGridStructureArray(cols);
                progressed = progressed ? true : SolveSudokuGridStructureArray(subgrids);
            }
            return grid.ToString();
        }

        /// <summary>
        /// Applies solving strategies to an array of grid structures (rows, columns, or subgrids)
        /// </summary>
        /// <param name="arr">Array of grid structures to solve</param>
        /// <returns>True if any progress was made in solving</returns>
        public static bool SolveSudokuGridStructureArray(SudokuGridStructure[] arr)
        {
            bool progressed = false;
            foreach (var structure in arr)
            {
                progressed = progressed ? true : structure.RemoveCandidates();
                progressed = progressed ? true : structure.SingleCandidate();
            }
            return progressed;
        }
    }
}
