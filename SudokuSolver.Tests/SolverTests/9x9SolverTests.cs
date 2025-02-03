using SudokuSolver.Core;
using System.ComponentModel;
using Xunit;

namespace SudokuSolver.Tests.SolverTests
{
    public class NineByNineSolverTests
    {
        public static TheoryData<string, string, string> ValidPuzzles =>
            new TheoryData<string, string, string>
            {
                { TestData.EasyInput, TestData.EasyOutput, "Easy" },
                { TestData.MediumInput, TestData.MediumOutput, "Medium" },
                { TestData.HardInput, TestData.HardOutput, "Hard" },
                { TestData.VeryHardInput, TestData.VeryHardOutput, "Very SHard" },
                { TestData.ExpertInput, TestData.ExpertOutput, "Expert" }
            };

        [Theory]
        [MemberData(nameof(ValidPuzzles))]
        [DisplayName("Solve {2} Puzzle")]
        public void TestAllDifficulties(
            string input, string expectedOutput, string difficulty)
        {
            var solver = new SudokuGridSolver(input);

            string actualOutput = solver.Solve();

            Assert.Equal(expectedOutput, actualOutput);
        }

    }
}
