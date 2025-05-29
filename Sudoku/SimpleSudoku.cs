using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sudoku;

public class SimpleSudoku : SudokuInterface
{
    private int[] _matrix = new int[81];

    public void generateBoard()
    {
        setBoard();
        shuffle();
    }

    private void setBoard()
    {
        for (int i = 0; i < 81; i++)
        {
            int row = i / 9;
            int col = i % 9;
            int b = row / 3;
            int val = (col + b + 3 * row) % 9;
            setCell(i, val);
        }
        
    }

    private void shuffle()
    {
        
    }

    public void setCell(int row, int col, int value)
    {
        if (row < 9 && col < 9 && value < 9 && row >= 0 && col >= 0 && value >= 0)
        {
            int index = row * 9 + col;

            _matrix[index] = value + 1;
        }
        else return;
    }

    public void setCell(int index, int value)
    {
        if (index >= 0 && index < 81)
        {
            _matrix[index] = value+1;
        }
        else return;
    }

    private int getCell(int row, int col)
    {
        int index = row * 9 + col;
        return _matrix[index];
    }

    private int getCell(int index)
    {
        return _matrix[index];
    }

    public string toString()
    {
        string result = "";
        string headFoot = String.Format("{0, 31}", "-------------------------------") + "\n";
        string line;

        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0) result += headFoot;
            line = "";
            for (int col = 0; col < 9; col++)
            {
                if (col % 3 == 0) line += "|";

                int value = getCell(row, col);
                line += String.Format("{0, -3}", String.Format("{0, 2}", value));
            }
            line += "|";
            result += line + "\n";
        }
        result += headFoot;


        return result;
    }
}
