using SudokuSolver.Core;
using System;
using System.IO;


namespace SudokuSolver.UI
{
    internal class FileUI
    {
        public static void HandleFile(ref string input)
        {
            if (!IsFile(input))
                return;
            SudokuGridValidator.ValidateFile(input);
            input = File.ReadAllText(input);
        }

        public static bool IsFile(string input)
        {
            return File.Exists(input);
        }
    }
}
