using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using Xunit;

namespace SudokuSolver.Tests.ValidatorTests
{
    public class SudokuValidationTests
    {
        /// <summary>
        /// Tests that an unsolvable puzzle throws the correct exception.
        /// </summary>
        [Fact]
        public void TestUnsolvablePuzzle()
        {
            string unsolvableInput = TestData.UnsolvableInput;

            UnsolvableSudokuGridException exception = Assert.Throws<UnsolvableSudokuGridException>(() =>
            {
                var solver = new SudokuGridSolver(unsolvableInput);
                solver.Solve();
            });
        }

        /// <summary>
        /// Tests that a grid with non-numeric characters throws the correct exception.
        /// </summary>
        [Fact]
        public void TestInvalidGridInputWithNonNumericCharacters()
        {
            string invalidInput = TestData.InputWithNonNumericCharacters;

            IllegalStringOfSudokuGridException exception = Assert.Throws<IllegalStringOfSudokuGridException>(() =>
            {
                SudokuGridValidator.ValidateLegalStringOfGrid(invalidInput);
            });
        }

        /// <summary>
        /// Tests that a grid with incorrect length throws the correct exception.
        /// </summary>
        [Fact]
        public void TestGridInputWithIncorrectLength()
        {
            string invalidLengthInput = TestData.InputWithIncorrectLength;

            IllegalStringOfSudokuGridException exception = Assert.Throws<IllegalStringOfSudokuGridException>(() =>
            {
                SudokuGridValidator.ValidateLegalStringOfGrid(invalidLengthInput);
            });
        }
    }
}
