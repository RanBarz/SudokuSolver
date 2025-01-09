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
        public void TestEasy()
        {
            string input = "530070000900060001000308000800090003004500200020006000000045000000080100070020005",
                output = "534678912976251348182349765813496275426815793759763124367128459214587639158934276";
            SudokuGrid tested = new SudokuGrid(input),
                expected = new SudokuGrid(output);
            tested.Solve();
            AssertEqual(expected, tested, "Easy test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a medium difficulty puzzle input.
        /// The input grid has fewer clues and requires more logical deduction to solve.
        /// </summary>
        public void TestMedium()
        {
            string input = "000400000800000000000002060100050000506000700000800005050100300000000002003004000",
                output = "276493158813256497945872361124657983569138724387419652652783149738921546491265837";
            SudokuGrid tested = new SudokuGrid(input),
                expected = new SudokuGrid(output);
            tested.Solve();
            AssertEqual(expected, tested, "Medium test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a hard difficulty puzzle input.
        /// This puzzle has fewer initial clues, requiring more complex strategies to solve.
        /// </summary>
        public void TestHard()
        {
            string input = "000000000000000100020005004600801007000030000000602004700200000008000000000000000",
                output = "817493562934671825562358471643821597791245638258976143475182936126534789389617254";
            SudokuGrid tested = new SudokuGrid(input),
                expected = new SudokuGrid(output);
            tested.Solve();
            AssertEqual(expected, tested, "Hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with a very hard puzzle input.
        /// The input has very few clues, making it extremely challenging to solve.
        /// </summary>
        public void TestVeryHard()
        {
            string input = "000000000000000080000050000500000006000040000000000050000000800000800000000000000",
                output = "123456789456789123789123456372894615598671234614357892937562841265418379841239567";
            SudokuGrid tested = new SudokuGrid(input),
                expected = new SudokuGrid(output);
            tested.Solve();
            AssertEqual(expected, tested, "Very hard test");
        }

        /// <summary>
        /// Tests the Sudoku solver with an expert-level puzzle input.
        /// The puzzle has an extremely low number of clues and is meant for advanced solvers.
        /// </summary>
        public void TestExpert()
        {
            string input = "000000000000000000500000000000000600000010800000000000000000000000000000000000000",
                output = "123487659476521398598639124319846275687315942245793861861254739752968413934172586";
            SudokuGrid tested = new SudokuGrid(input),
                expected = new SudokuGrid(output);
            tested.Solve();
            AssertEqual(expected, tested, "Expert test");
        }

        /// <summary>
        /// This method shows the result of a test.
        /// </summary>
        public void AssertEqual(SudokuGrid excpected, SudokuGrid actual, string testName)
        {
            if (excpected.ToString() == actual.ToString())
                Console.WriteLine($"{testName} passed!");
            else
                Console.WriteLine($"{testName} failed: excpected {excpected}\n" +
                    $", but got {actual}.");
        }
    }
}
