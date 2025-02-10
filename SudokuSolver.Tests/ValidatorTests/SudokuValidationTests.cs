using SudokuSolver.Core;
using SudokuSolver.Core.Exceptions;
using SudokuSolver.Core.Helpers;
using SudokuSolver.UI;

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
                SudokuStringValidator.ValidateLegalStringOfGrid(invalidInput);
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
                SudokuStringValidator.ValidateLegalStringOfGrid(invalidLengthInput);
            });
        }

        /// <summary>
        /// This method tests if the solver raises Invalid for an Invalid grid.
        /// </summary>
        [Fact]
        public void TestInvalidSudokuGrid()
        {
            string input = TestData.InvalidSudoku;

            InvalidSudokuGridException exception = Assert.Throws<InvalidSudokuGridException>(() =>
            {
                var solver = new SudokuGridSolver(input);
                solver.Solve();
            });
        }

        /// <summary>
        /// This method test a file with wrong extension
        /// </summary>
        [Fact]
        public void TestFileExtension()
        {
            IllegalFileOfSudokuGridException exception = Assert.Throws<IllegalFileOfSudokuGridException>(() =>
            {
                SudokuStringValidator.ValidateFile(TestData.FileExtension);
            });       
        }

        /// <summary>
        /// Tests that a grid with too long length throws the correct exception.
        /// </summary>
        [Fact]
        public void TestTooLongGrid()
        {
            string invalidLengthInput = TestData.InputWithTooLongLength;

            IllegalStringOfSudokuGridException exception = Assert.Throws<IllegalStringOfSudokuGridException>(() =>
            {
                SudokuStringValidator.ValidateLegalStringOfGrid(invalidLengthInput);
            });
        }

        [Fact]
        public void TestEmptyInput()
        {
            string input = TestData.EmptyInput;
            bool unnecessary;
            ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            {
                SudokuStringValidator.ValidateInput(input);
            });
            
        }
    }
}
