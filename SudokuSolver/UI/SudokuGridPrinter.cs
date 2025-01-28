using SudokuSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.UI
{
    internal static class SudokuGridPrinter
    {
        /// <summary>
        /// A method which can print a sudoku grid of any size.
        /// </summary>
        public static void PrintSudokuGrid(string grid)
        {
            int gridSize = SudokuGridSolver.GetGridSize(grid);

            PrintSudokuGridBorder(gridSize, true);

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write("║ ");
                for (int col = 0; col < gridSize; col++)
                {
                    char value = grid[row * gridSize + col];
                    Console.Write(value == '0' ? '·' : value);

                    if (col < gridSize - 1)
                    {
                        Console.Write(" ");
                        if ((col + 1) % Math.Sqrt(gridSize) == 0)
                        {
                            Console.Write("│ ");
                        }
                    }
                }
                Console.WriteLine(" ║");

                if (row < gridSize - 1 && (row + 1) % Math.Sqrt(gridSize) == 0)
                {
                    PrintSudokuSubgridBorder(gridSize);
                }
            }
            PrintSudokuGridBorder(gridSize, false);
        }

        /// <summary>
        /// A method which prints the grid's borders.
        /// </summary>
        public static void PrintSudokuGridBorder(int gridSize, bool isUpperBorder)
        {
            int subgridSize = (int)Math.Sqrt(gridSize);
            if (isUpperBorder)
                Console.Write("╔");
            else
                Console.Write("╚");
            for (int i = 0; i < (subgridSize * 2 + 1) * subgridSize + subgridSize - 1; i++)
                if (i % subgridSize * 2 + 2 == 0)
                    if (isUpperBorder)
                        Console.Write("╤");
                    else
                        Console.Write("╧");
                else
                    Console.Write("═");
            if (isUpperBorder)
                Console.WriteLine("╗");
            else
                Console.WriteLine("╝");
        }

        /// <summary>
        /// A method which prints the subgrids' borders.
        /// </summary>
        public static void PrintSudokuSubgridBorder(int gridSize)
        {
            int subgridSize = (int)Math.Sqrt(gridSize);

            Console.Write("╟");
            for (int i = 0; i < (subgridSize * 2 + 1) * subgridSize + subgridSize - 1; i++)
                if (i % subgridSize * 2 + 2 == 0)
                    Console.Write("┼");
                else
                    Console.Write("─");
            Console.WriteLine("╢");
        }
    }
}
