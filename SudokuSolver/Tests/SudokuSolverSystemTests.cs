using SudokuSolver.Core;
using SudokuSolver.UI;
using System;

namespace SudokuSolver.Tests
{
    /// <summary>
    /// This class tests the SudokuSolver system by trying the SudokuGrid Solve method.
    /// </summary>
    internal class SudokuSolverSystemTests
    {
        /// <summary>
        /// Tests the Sudoku solver with an easy puzzle input.
        /// The input grid has a moderate number of filled cells, making it easier to solve.
        /// </summary>
        public static void TestEasy()
        {
            string expected = TestData.EasyOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.EasyInput);
            AssertEqual(expected, actual.Solve(), "Easy test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a medium difficulty puzzle input.
        /// The input grid has fewer clues and requires more logical deduction to solve.
        /// </summary>
        public static void TestMedium()
        {
            string expected = TestData.MediumOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.MediumInput);
            AssertEqual(expected, actual.Solve(), "Medium test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a hard difficulty puzzle input.
        /// This puzzle has fewer initial clues, requiring more complex strategies to solve.
        /// </summary>
        public static void TestHard()
        {
            string expected = TestData.HardOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.HardInput);
            AssertEqual(expected, actual.Solve(), "Hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a very hard puzzle input.
        /// The input has very few clues, making it extremely challenging to solve.
        /// </summary>
        public static void TestVeryHard()
        {
            string expected = TestData.VeryHardOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.VeryHardInput);
            AssertEqual(expected, actual.Solve(), "Very Hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with an expert-level puzzle input.
        /// The puzzle has an extremely low number of clues and is meant for advanced solvers.
        /// </summary>
        public static void TestExpert()
        {
            string expected = TestData.ExpertOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.ExpertInput);
            AssertEqual(expected, actual.Solve(), "Expert test");
        }

        /// <summary>
        /// Compares the expected and actual Sudoku grids, logging the result of the test.
        /// </summary>
        public static void AssertEqual(string expected, string actual, string testName)
        {
            if (expected.Equals(actual))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                SudokuUI.PrintGreen($"{testName} passed!");
            }
            else
                SudokuUI.PrintRed($"{testName} failed: expected {expected}\n" +
                    $", but got {actual}.");
        }
    }
}
