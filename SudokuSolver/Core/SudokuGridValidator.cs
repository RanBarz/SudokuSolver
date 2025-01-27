using SudokuSolver.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    /// <summary>
    /// The class offering validation to both string representation of sudoku grids,
    /// and objects of them.
    /// </summary>
    internal class SudokuGridValidator
    {
        private const string FILE_PATH_EXTENSION = ".txt";
        private const int LARGEST_GRID = 25;
        /// <summary>
        /// Returns true if an unsolved cell has no candidates.
        /// </summary>
        public static bool IsUnsolvable(SudokuGrid grid)
        {
            foreach (SudokuGridStructure row in grid.GetRows())
                if (row.HasUnsolvableCell())
                    return true;
            return false;
        }

        /// <summary>
        /// Returns true if the grid has duplicates in one of its structures.
        /// </summary>
        public static bool IsInvalid(SudokuGrid grid)
        {
            bool hasDuplicates;

            hasDuplicates = GridStructureArrayHasDuplicates(grid.GetRows());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetCols());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetSubgrids());
            return hasDuplicates;
        }

        /// <summary>
        /// A method which receives an array of grid structures. and returns true if one of them has a
        /// duplicate value.
        /// </summary>
        public static bool GridStructureArrayHasDuplicates(SudokuGridStructure[] arr)
        {
            foreach (SudokuGridStructure structure in arr)
                if (structure.HasDuplicates())
                    return true;
            return false;
        }

        /// <summary>
        /// A method which throws an exception if a string representation of a grid isn't legal.
        /// </summary>
        public static void ValidateLegalStringOfGrid(string grid)
        {
            int gridSize = ValidateHasProperSize(grid);
            ValidateHasProperChars(grid, gridSize);
        }

        /// <summary>
        /// A method which throws an exception if a string representation of a grid isn't of legal size.
        /// </summary>
        public static int ValidateHasProperSize(string grid)
        {
            double rootOfLength = Math.Sqrt(grid.Length);
            if (Math.Sqrt(rootOfLength) % 1 != 0 || rootOfLength > LARGEST_GRID)
                throw new IllegalStringOfSudokuGridException(
                    $"The string reprsentation of the grid has {grid.Length} characters, " +
                    $"which square root isn't a natural number..");
            return (int) rootOfLength;
        }

        /// <summary>
        /// A method which throws an exception if a string representation has illegal chars.
        /// </summary>
        public static void ValidateHasProperChars(string grid, int gridSize)
        {
            int value;

            foreach (char c in grid)
            {
                value = c - '0';
                if (value < 0 || value > gridSize)
                    throw new IllegalStringOfSudokuGridException(
                        $"The character {c} isn't legal in a Sudoku grid.");
            }
        }

        /// <summary>
        /// A method which throws an exception if the input is null
        /// </summary>
        public static void ValidateInput(string input)
        {
            if (input == null)
                throw new ArgumentException("The input must be a Sudoku grid, " +
                    "following the instructions.");
        }

        /// <summary>
        /// A method which throws an exception if the number of parameters is below the min, 
        /// or above the max.
        /// </summary>
        public static void ValidateParameters(string[] parameters, int maxParameters, int minParameters)
        {
            if (TooManyParams(parameters, maxParameters))
                throw new ArgumentException("Too many arguments provided. Only up to 2 are allowed.");
            if (!EnoughParams(parameters, minParameters))
                throw new ArgumentException("Not enough arguments provided. At least one is required.");
        }

        public static bool TooManyParams(string[] parameters, int maxParameters) 
            => parameters.Length > maxParameters;
    
        public static bool EnoughParams(string[] parameters, int minParameters) 
            => parameters.Length >= minParameters;

        /// <summary>
        /// A method which validates that a file is of the right type for Sudoku.
        /// </summary>
        /// <exception cref="IllegalFileOfSudokuGridException"></exception>
        public static void ValidateFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            if (!extension.Equals(FILE_PATH_EXTENSION))
                throw new IllegalFileOfSudokuGridException("The file containing" +
                    " the grid must have '.txt' extension");
        }
    }
}
