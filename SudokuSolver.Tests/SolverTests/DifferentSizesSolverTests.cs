using SudokuSolver.Core;
using Xunit;

namespace SudokuSolver.Tests.SolverTests
{
    public class DifferentSizesSolverTests
    {
        /// <summary>
        /// Tests the Sudoku solver with an 1x1 puzzle input.
        /// </summary>
        [Fact]
        public void TestSizeOne()
        {
            string expected = TestData.OneByOneOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.OneByOneInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a 4x4 difficulty puzzle input.
        /// </summary>
        [Fact]
        public void TestSizeFour()
        {
            string expected = TestData.FourByFourOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.FourByFourInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a 16x16 difficulty puzzle input.
        /// </summary>
        [Fact]
        public void TestSizeSixteen()
        {
            string expected = TestData.SixteenBySixteenOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.SixteenBySixteenInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a 25x25 puzzle input.
        /// </summary>
        [Fact]
        public void TestSizeTwentyFive()
        {
            string expected = TestData.TwentyFiveOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.TwentyFiveInput);
            Assert.Equal(expected, actual.Solve());
        }
    }
}
