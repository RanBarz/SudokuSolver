# SudokuSolver  

A fast and efficient Sudoku solver that minimizes backtracking by applying advanced solving techniques. The solver supports various grid sizes and allows input via direct string representation or file paths.  

I have not encountered a grid smaller than **25×25** that my solver cannot solve in under a second.  

## Features  
- **Advanced Heuristics:** Implements strategies like Naked Singles, Hidden Singles, and Naked Pairs through Naked Eights to reduce reliance on backtracking.  
- **Flexible Input:** Accepts Sudoku puzzles either as a file or a string representation.  
- **Multiple Grid Sizes:** Supports 1×1, 4×4, 9×9, 16×16, and 25×25 grids.  
- **Optimized Performance:** Reduces brute-force solving by prioritizing constraint-based techniques.  

### File Input  
- Enter the name of a `.txt` file containing the Sudoku grid.  
- The file path **must not contain spaces**.  
- The file content must follow the same format as the string representation (see below).  

### String Input  
- Enter a string with exactly as many characters as the number of cells in the grid.  
- The largest acceptable grid is **25×25**.  
- **Empty cells** are represented by `0`.  
- **Filled cells** use characters where the number is the ASCII difference from `0`.  

## Example  
For a **4×4** grid:  
```
1032000040000000
```
This represents:  
```
1  0  3  2  
0  0  0  0  
4  0  0  0  
0  0  0  0  
```
