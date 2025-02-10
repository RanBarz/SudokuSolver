using SudokuSolver.Core;
using SudokuSolver.UI.Output;
﻿using SudokuSolver.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UI.Input
{
    internal static class InputHandler
    {
        /// <summary>
        /// A method which gets the input from the console, validates it, and handles it as a file as well.
        /// </summary>
        public static void GetInput(ref string input, ref bool graphicMode)
        {
            OutputHandler.PrintBlue(UIConstants.MenuMessage);
            input = Console.ReadLine();
            input = HandleInput(input, ref graphicMode);
            Console.Write("\f\u001bc\x1b[3J");
            if (input.Equals("exit"))
                return;
            FileUI.HandleFile(ref input);
            SudokuStringValidator.ValidateLegalStringOfGrid(input);
            OutputHandler.PrintBlue(UIConstants.ShowInputMessage);
            if (graphicMode)
                SudokuGridPrinter.PrintSudokuGrid(input);
            else
                OutputHandler.PrintBlue(input);
        }

        /// <summary>
        /// A method which checks the parameters of the input: grid / file path, and -s / nothing.
        /// </summary>
        public static string HandleInput(string input, ref bool graphicMode)
        {
            string[] parameters;

            SudokuStringValidator.ValidateInput(input);
            parameters = input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            SudokuStringValidator.ValidateParameters(parameters, UIConstants.MaxParameters, UIConstants.MinParameters);
            input = parameters[UIConstants.SudokuGridIndex];
            graphicMode = !SudokuStringValidator.ValidateSecondParam(parameters);
            return input;
        }
    }

}
