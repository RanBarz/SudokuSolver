using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UI
{
    internal class UIConstants
    {
        public const int MAX_PARAMETERS = 2, MIN_PARAMETERS = 1;
        public const string START_MESSAGE = "Welcome to the Omega Sudoku Solver.\n" +
            "Instructions:\n" +
            "-\tYou can either enter a string that represents a grid, or a file path.\n" +
            "-\tFile:\n" +
            "\t-\tEnter the name of a txt file.\n" +
            "\t-\tThe file path mustn't have spaces in it.\n" +
            "\t-\tThe text inside the file must follow the rules of a string representation.\n" +
            "-\tString:\n" +
            "\t-\tEnter a string which has the same amount of characters as " +
            "the number of cells in your Sudoku grid.\n" +
            "\t-\tZero represents an empty cell.\n" +
            "\t-\tEach character represents a number that is its difference from Zero in ascii.\n" +
            "\t-\tThe largest grid acceptable is 25x25.\n"
        , MENU_MESSAGE = "\nEnter a Sudoku grid (or 'exit', " +
            "you can add ' -s' for string representation):"
        , RESULT_MESSAGE = "\nThe solution to this grid is: "
        , SHOW_INPUT_MESSAGE = "The grid you entered looks as follows: ";

    }
}
