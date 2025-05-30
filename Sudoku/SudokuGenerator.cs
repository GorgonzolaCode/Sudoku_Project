using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Sudoku;

public class SudokuGenerator : SudokuInterface
{
        Random random = new Random();


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
            setCell(i, val+1);
        }

    }

    private void shuffle()
    {
        //shuffleCols();
        //shuffleColBlocks()
        //shuffleRows()
        //shuffleRowBlocks()
        //swapPartialCols()
        //swapPartialRows()
        //swapNumbers()
    }

    private void shuffleColBlocks()
    {
        int ranNum = random.Next(4);

        switch (ranNum)
        {
            case 0: swapBlocks(0, 0, 0, 1);
                break;
            case 1: swapBlocks(0, 0, 0, 2);
                break;
            case 2: swapBlocks(0, 0, 1, 2);
                break;
            
            default: return;
        }

    }

    private void shuffleRowBlocks()
    {
        int ranNum = random.Next(4);

        switch (ranNum)
        {
            case 0: swapBlocks(0, 1, 0, 0);
                break;
            case 1: swapBlocks(0, 2, 0, 0);
                break;
            case 2: swapBlocks(1, 2, 0, 0);
                break;
            
            default: return;
        }

    }

    public void swapColBlocks(int c1, int c2)
    public void swapColBlocks(int c1, int c2)
    {
        if (c1 < 0 || c2 < 0 || c1 >= 3 || c2 >= 3)
            throw new Exception(String.Format("Swapped columns do not exist: {0}, {1}", c1, c2));

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                int temp1 = getCell(row, col + 3 * c1);
                int temp2 = getCell(row, col + 3 * c2);
                setCell(row, col + 3 * c1, temp2);
                setCell(row, col + 3 * c2, temp1);
            }
        }
    }

    public void swapBlocks(int r1, int r2, int c1, int c2)
    public void swapBlocks(int r1, int r2, int c1, int c2)
    {
        if (!isValidSwapArg(r1,r2,c1,c2))
            throw new Exception(String.Format("Swap-arguments are not valid"));
            throw new Exception(String.Format("Swap-arguments are not valid"));

        int rowLimit = 3;
        int colLimit = 3;
        if (r1 == r2 && r1 == 0) rowLimit = 9;
        if (c1 == c2 && c1 == 0) colLimit = 9;
        if (r1 == r2 && r1 == 0) rowLimit = 9;
        if (c1 == c2 && c1 == 0) colLimit = 9;

        for (int row = 0; row < rowLimit; row++)
        {
            for (int col = 0; col < colLimit; col++)
            {
                int temp1 = getCell(row + 3 * r1, col + 3 * c1);
                int temp2 = getCell(row + 3 * r2, col + 3 * c2);
                setCell(row + 3 * r1, col + 3 * c1, temp2);
                setCell(row + 3 * r2, col + 3 * c2, temp1);
                int temp2 = getCell(row + 3 * r2, col + 3 * c2);
                setCell(row + 3 * r1, col + 3 * c1, temp2);
                setCell(row + 3 * r2, col + 3 * c2, temp1);
            }
        }
    }

    public static bool isValidSwapArg(int r1, int r2, int c1, int c2)
    public static bool isValidSwapArg(int r1, int r2, int c1, int c2)
    {
        if (r1 < 0 || r2 < 0 || c1 < 0 || c2 < 0) return false;
        if (r1 >= 3 || r2 >= 3 || c1 >= 3 || c2 >= 3) return false;
        if (r1 != 0 || r2 != 0)
        {
            if (c1 != 0 || c2 != 0) return false;
        }
        return true;
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
            _matrix[index] = value;
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

    public override string ToString()
    {
        string result = "";
        string headFoot = String.Format("{0, 31}", "-------------------------------") + Environment.NewLine;
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
            result += line + Environment.NewLine;
        }
        result += headFoot;


        return result;
    }
    


}
