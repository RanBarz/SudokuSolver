using SudokuSolver.Core.Helpers;
using System;
using System.IO;


namespace SudokuSolver.UI
{
    internal class FileUI
    {
        /// <summary>
        /// A method which handles getting the input, if it is a file.
        /// </summary>
        public static void HandleFile(ref string input)
        {
            if (!IsFile(input))
                return;
            SudokuGridValidator.ValidateFile(input);
            input = File.ReadAllText(input);
        }

        /// <summary>
        /// A method which makes sure the file exists.
        /// </summary>
        public static bool IsFile(string input)
        {
            return File.Exists(input);
        }
    }
}
