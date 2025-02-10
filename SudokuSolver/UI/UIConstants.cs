using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UI
{
    internal class UIConstants
    {
        public const int MaxParameters = 2, MinParameters = 1, 
            SudokuGridIndex = 0, LargestGrid = 25, SecondParamIndex = 1;
        public const string StartMessage = "Welcome to the Omega Sudoku Solver.\n" +
            "Instructions:\n" +
            "-\tYou can either enter a string that represents a grid, or a file path.\n" +
            "-\tFile:\n" +
            "\t-\tEnter the name of a txt file.\n" +
            "\t-\tThe file path mustn't have spaces in it.\n" +
            "\t-\tThe text inside the file must follow the rules of a string representation.\n" +
            "-\tString:\n" +
            "\t-\tEnter a string which has the same amount of characters as " +
            "the number of cells in your Sudoku grid.\n" +
            "\t\tSizes: 1x1, 4x4, 9x9, 16x16 and 25x25.\n" +
            "\t-\tZero represents an empty cell.\n" +
            "\t-\tEach character represents a number that is its difference from Zero in ascii.\n" +
            "\t-\tThe largest grid acceptable is 25x25.\n"
        , MenuMessage = "\nEnter a Sudoku grid (or 'exit', " +
            "you can add ' -s' for string representation):"
        , ResultMessage = "\nThe solution to this grid is: "
        , ShowInputMessage = "The grid you entered looks as follows: "
        , FILE_PATH_EXTENSION = ".txt"
        , AcceptableSecondParam = "-s";

    }
}
