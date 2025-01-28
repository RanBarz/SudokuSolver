using SudokuSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UI
{
    internal static class InputHandler
    {
        /// <summary>
        /// A method which gets the input from the console, validates it, and handles it as a file as well.
        /// </summary>
        public static void GetInput(ref string input, ref bool graphicMode)
        {
            SudokuUI.PrintBlue(UIConstants.MENU_MESSAGE);
            input = Console.ReadLine();
            input = HandleInput(input, ref graphicMode);
            Console.Write("\f\u001bc\x1b[3J");
            if (input.Equals("exit"))
                return;
            FileUI.HandleFile(ref input);
            SudokuGridValidator.ValidateLegalStringOfGrid(input);
            SudokuUI.PrintBlue(UIConstants.SHOW_INPUT_MESSAGE);
            if (graphicMode)
                SudokuGridPrinter.PrintSudokuGrid(input);
            else
                SudokuUI.PrintBlue(input);
        }

        /// <summary>
        /// A method which checks the parameters of the input: grid / file path, and -s / nothing.
        /// </summary>
        public static string HandleInput(string input, ref bool graphicMode)
        {
            string[] parameters;

            SudokuGridValidator.ValidateInput(input);
            parameters = input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            SudokuGridValidator.ValidateParameters(parameters, UIConstants.MAX_PARAMETERS, UIConstants.MIN_PARAMETERS);
            input = parameters[0];
            if (parameters.Length > 1)
            {
                if (parameters[1] == "-s")
                    graphicMode = false;
                else
                    throw new ArgumentException("The only acceptable second param is -s.");
            }
            else
                graphicMode = true;
            return input;
        }
    }

}
