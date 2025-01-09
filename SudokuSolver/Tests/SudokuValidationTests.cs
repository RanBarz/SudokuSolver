using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Tests
{
    internal class SudokuValidationTests
    {
        /// <summary>
        /// Provides system tests for validating Sudoku puzzle inputs and handling edge cases.
        /// </summary>
        public void TestUnsolvablePuzzle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                SudokuGrid tested = new SudokuGrid(TestData.UnsolvableInput);
                tested.Solve();
                Console.WriteLine("Unsolvable puzzle test failed: No exception was thrown.");
            }
            catch (UnsolvablePuzzleException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Unsolvable puzzle test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unsolvable puzzle test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Validates that an exception is thrown for the invalid grid input case.
        /// </summary>
        public void TestInvalidGridInputWithNonNumericCharacters()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                SudokuGrid tested = new SudokuGrid(TestData.InvalidInput1);
                Console.WriteLine("Invalid grid input test failed: No exception was thrown.");
            }
            catch (InvalidGridInputException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Invalid grid input test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid grid input test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Validates that an exception is thrown for grid that doesn't follow Sudoku rules.
        /// </summary>
        public void TestInvalidGridInputWithIncorrectLength()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                SudokuGrid tested = new SudokuGrid(TestData.InvalidInput2);
                Console.WriteLine("Invalid grid input test failed: No exception was thrown.");
            }
            catch (InvalidGridInputException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Invalid grid input test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid grid input test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }
    }
}
