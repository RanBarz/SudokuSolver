using SudokuSolver.Core;
using System.ComponentModel;

namespace SudokuSolver.Tests.SolverTests
{
    public class DifferentSizesSolverTests
    {
        public static TheoryData<string, string, string> ValidPuzzles =>
            new TheoryData<string, string, string>
            {
                { TestData.OneByOneInput, TestData.OneByOneOutput, "1x1" },
                { TestData.FourByFourInput, TestData.FourByFourOutput, "4x4" },
                { TestData.SixteenBySixteenInput, TestData.SixteenBySixteenOutput, "16x16" },
                { TestData.TwentyFiveInput, TestData.TwentyFiveOutput, "25x25" }
            };

        [Theory]
        [MemberData(nameof(ValidPuzzles))]
        [DisplayName("Solve {2} Puzzle")]
        public void TestDifferentSizes(
            string input, string expectedOutput, string puzzleSize)
        {
            var solver = new SudokuGridSolver(input);

            string actualOutput = solver.Solve();

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}