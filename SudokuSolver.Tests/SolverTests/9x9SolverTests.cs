using SudokuSolver.Core;
using System.ComponentModel;
using System.Diagnostics;

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
                { TestData.ExpertInput, TestData.ExpertOutput, "Expert" },
                { TestData.FullGridInput, TestData.FullGridInput, "Full Grid" },
                { TestData.EmptyGridInput, TestData.EmptyGridOutput, "Emtpy Grid" },
                { TestData.AlmostFullGridInput, TestData.AlmostFullGridOutput, "Almost Full" }
            };

        /// <summary>
        /// A test for each difficulty of a 9x9 grid.
        /// </summary>
        [Theory]
        [MemberData(nameof(ValidPuzzles))]
        [DisplayName("Solve {2} Puzzle")]
        public void TestAllDifficulties(
            string input, string expectedOutput, string difficulty)
        {
            var stopwatch = Stopwatch.StartNew();

            var solver = new SudokuGridSolver(input);

            string actualOutput = solver.Solve();

            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds <= TestData.MaxTimeInMs);

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
