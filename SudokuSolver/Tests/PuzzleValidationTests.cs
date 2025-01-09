using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Tests
{
    internal class PuzzleValidationTests
    {
        /// <summary>
        /// Tests that an exception is thrown when an invalid Sudoku puzzle is passed to the solver.
        /// </summary>
        public void TestUnsolvablePuzzle()
        {
            string invalidInput = "0933000001000804600000008004506003000327560006001094001000005800200002000760\r\n";
            SudokuGrid tested = new SudokuGrid(invalidInput);
            try
            {
                tested.Solve();
                Console.WriteLine("Unsolvable puzzle test failed: " +
                    "No exception was thrown.");
            }
            catch (UnsolvablePuzzleException ex)
            {
                Console.WriteLine("Test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }

    }
}
