using System;
using System.Numerics;

namespace Sudoku;

public interface SudokuInterface
{

    public void setCell(int row, int col, int value);
    public void generateBoard();


    public abstract string ToString();

    public void print()
    {
        Console.WriteLine(ToString());
    }
}
