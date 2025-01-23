using SudokuSolver.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    internal class SudokuGridValidator
    {
        public static bool IsUnsolvable(SudokuGrid grid)
        {
            foreach (var row in grid.GetRows())
                if (row.HasUnsolvableCell())
                    return true;
            return false;
        }

        public static bool IsInvalid(SudokuGrid grid)
        {
            bool hasDuplicates = false;

            hasDuplicates = GridStructureArrayHasDuplicates(grid.GetRows());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetCols());
            hasDuplicates = hasDuplicates || 
                GridStructureArrayHasDuplicates(grid.GetSubgrids());
            return hasDuplicates;
        }

        public static bool GridStructureArrayHasDuplicates(SudokuGridStructure[] arr)
        {
            foreach (var structure in arr)
                if (structure.HasDuplicates())
                    return true;
            return false;
        }

        public static void ValidateLegalStringOfGrid(string grid, int gridSize)
        {
            ValidateHasProperSize(grid, gridSize);
            ValidateHasProperChars(grid, gridSize);
        }

        public static void ValidateHasProperSize(string grid, int gridSize)
        {
            if (grid.Length != gridSize * gridSize)
                throw new IllegalStringOfSudokuGridException(
                    $"The string reprsentation of the grid has {grid.Length} characters, " +
                    $"instead of {gridSize * gridSize}.");
        }

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

        public static void ValidateParameters(string[] parameters, int maxParameters, int minParameters)
        {
            if (TooManyParams(parameters, maxParameters))
                throw new ArgumentException("Too many arguments provided. Only up to 2 are allowed.");
            if (!EnoughParams(parameters, minParameters))
                throw new ArgumentException("Not enough arguments provided. At least one is required.");
        }

        public static bool TooManyParams(string[] parameters, int maxParameters) => parameters.Length > maxParameters;
    
        public static bool EnoughParams(string[] parameters, int minParameters) => parameters.Length >= minParameters;
    }
}
