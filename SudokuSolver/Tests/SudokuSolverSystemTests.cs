using SudokuSolver.Core;
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
            SudokuGrid actual = new SudokuGrid(TestData.EasyInput),
                expected = new SudokuGrid(TestData.EasyOutput);
            actual.Solve();
            AssertEqual(expected, actual, "Easy test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a medium difficulty puzzle input.
        /// The input grid has fewer clues and requires more logical deduction to solve.
        /// </summary>
        public static void TestMedium()
        {
            SudokuGrid actual = new SudokuGrid(TestData.MediumInput),
                expected = new SudokuGrid(TestData.MediumOutput);
            actual.Solve();
            AssertEqual(expected, actual, "Medium test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a hard difficulty puzzle input.
        /// This puzzle has fewer initial clues, requiring more complex strategies to solve.
        /// </summary>
        public static void TestHard()
        {
            SudokuGrid actual = new SudokuGrid(TestData.HardInput),
                expected = new SudokuGrid(TestData.HardOutput);
            actual.Solve();
            AssertEqual(expected, actual, "Hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a very hard puzzle input.
        /// The input has very few clues, making it extremely challenging to solve.
        /// </summary>
        public static void TestVeryHard()
        {
            SudokuGrid actual = new SudokuGrid(TestData.VeryHardInput),
                expected = new SudokuGrid(TestData.VeryHardOutput);
            actual.Solve();
            AssertEqual(expected, actual, "Very hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with an expert-level puzzle input.
        /// The puzzle has an extremely low number of clues and is meant for advanced solvers.
        /// </summary>
        public static void TestExpert()
        {
            SudokuGrid actual = new SudokuGrid(TestData.ExpertInput),
                expected = new SudokuGrid(TestData.ExpertOutput);
            actual.Solve();
            AssertEqual(expected, actual, "Expert test");
        }

        /// <summary>
        /// Compares the expected and actual Sudoku grids, logging the result of the test.
        /// </summary>
        public static void AssertEqual(SudokuGrid expected, SudokuGrid actual, string testName)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (expected.ToString() == actual.ToString())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{testName} passed!");
            }
            else
                Console.WriteLine($"{testName} failed: expected {expected}\n" +
                    $", but got {actual}.");
        }
    }
}
