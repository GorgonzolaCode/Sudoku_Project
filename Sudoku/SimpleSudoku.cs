using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sudoku;

public class SimpleSudoku : SudokuInterface
{
    private int[] _matrix = new int[81];

    public void generateBoard()
    {
        throw new NotImplementedException();
    }

    private void setBoard()
    {
        for (int i = 0; i < 81; i++)
        {
            setCell(i, 0);
        }
    }

    public void setCell(int row, int col, int value)
    {
        if (row < 9 && col < 9 && value < 9 && row >= 0 && col >= 0 && value >= 0)
        {
            int index = row * 9 + col;

            _matrix[index] = value;
        }
        else return;
    }

    public void setCell(int index, int value)
    {
        if (index >= 0 && index < 81)
        {
            
        }
    }

    private int getCell(int row, int col)
    {
        int index = row * 9 + col;
        return _matrix[index];
    }

    public string toString()
    {
        string result = "";

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                int value = getCell(row, col);
                result += String.Format("0, -4", value);
            }
            result += "\n";
        }

        return result;
    }
}
