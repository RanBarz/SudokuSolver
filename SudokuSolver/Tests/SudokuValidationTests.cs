using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.UI;
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
        public static void TestUnsolvablePuzzle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                SudokuGridSolver tested = new SudokuGridSolver(TestData.UnsolvableInput);
                tested.Solve();
                ConsoleUI.PrintRed("Unsolvable puzzle test failed: No exception was thrown.");
            }
            catch (UnsolvableSudokuGridException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleUI.PrintGreen("Unsolvable puzzle test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                ConsoleUI.PrintRed($"Unsolvable puzzle test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Validates that an exception is thrown for the invalid grid input case.
        /// </summary>
        public static void TestInvalidGridInputWithNonNumericCharacters()
        {
            try
            {
                SudokuGrid tested = new SudokuGrid(TestData.InvalidInput1);
                ConsoleUI.PrintRed("Invalid grid input test failed: No exception was thrown.");
            }
            catch (InvalidSudokuGridException)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleUI.PrintGreen("Invalid grid input test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                ConsoleUI.PrintRed($"Invalid grid input test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }

        /// <summary>
        /// Validates that an exception is thrown for grid that doesn't follow Sudoku rules.
        /// </summary>
        public static void TestInvalidGridInputWithIncorrectLength()
        {
            try
            {
                SudokuGrid tested = new SudokuGrid(TestData.InvalidInput2);
                ConsoleUI.PrintRed("Invalid grid input test failed: No exception was thrown.");
            }
            catch (InvalidSudokuGridException)
            {
                ConsoleUI.PrintGreen("Invalid grid input test passed: Correct exception thrown.");
            }
            catch (Exception ex)
            {
                ConsoleUI.PrintRed($"Invalid grid input test failed: Unexpected exception type: {ex.GetType().Name}");
            }
        }
    }
}
