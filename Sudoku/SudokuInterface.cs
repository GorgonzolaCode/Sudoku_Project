using System;
using System.Numerics;

namespace Sudoku;

public interface SudokuInterface
{

    void setCell(int row, int col, int value);
    void generateBoard();



    string toString();
    void print()
    {
        Console.WriteLine(toString());
    }
}
