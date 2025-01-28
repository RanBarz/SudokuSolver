using SudokuSolver.Core;
using Xunit;

namespace SudokuSolver.Tests.SolverTests
{
    public class NineByNineSolverTests
    {
        /// <summary>
        /// Tests the Sudoku solver with an easy puzzle input.
        /// </summary>
        [Fact]
        public void TestEasy()
        {
            string expected = TestData.EasyOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.EasyInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a medium difficulty puzzle input.
        /// </summary>
        [Fact]
        public void TestMedium()
        {
            string expected = TestData.MediumOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.MediumInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a hard difficulty puzzle input.
        /// </summary>
        [Fact]
        public void TestHard()
        {
            string expected = TestData.HardOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.HardInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with a very hard puzzle input.
        /// </summary>
        [Fact]
        public void TestVeryHard()
        {
            string expected = TestData.VeryHardOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.VeryHardInput);
            Assert.Equal(expected, actual.Solve());
        }

        /// <summary>
        /// Tests the Sudoku solver with an expert-level puzzle input.
        /// </summary>
        [Fact]
        public void TestExpert()
        {
            string expected = TestData.ExpertOutput;
            SudokuGridSolver actual = new SudokuGridSolver(TestData.ExpertInput);
            Assert.Equal(expected, actual.Solve());
        }
    }
}
