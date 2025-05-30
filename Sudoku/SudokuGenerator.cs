using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
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

    public void setBoard()
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

    public bool isValid()
    {
        return blocksValid() && rowsValid() && colsValid();
    }

    public bool colsValid()
    {
        bool[] found;

        for (int col = 0; col < 9; col++)
        {
            found = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                int value = getCell(i, col);
                if (value == 0) continue;
                if (found[value - 1]) return false;
                found[value - 1] = true;
            }
        }

        return true;
    }

    public bool rowsValid()
    {
        bool[] found;

        for (int row = 0; row < 9; row++)
        {
            found = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                int value = getCell(row, i);
                if (value == 0) continue;
                if (found[value - 1]) return false;
                found[value - 1] = true;
            }
        }

        return true;
    }

    public bool blocksValid()
    {
        bool[] found;

        for (int block = 0; block < 9; block++)
        {
            int firstPosCol = block % 3 * 3;
            int firstPosRow = block / 3 * 3;

            found = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                int curCol = firstPosCol + i % 3;
                int curRow = firstPosRow + i / 3;

                int value = getCell(curRow, curCol);
                if (value == 0) continue;
                if (found[value - 1]) return false;
                found[value - 1] = true;
            }
        }

        return true;
    }



    private void shuffle()
    {
        shuffleCols();
        shuffleColBlocks();
        shuffleRows();
        shuffleRowBlocks();
        //swapPartialCols();
        //swapPartialRows();
        //swapNumbers();
    }

    private void shuffleCols()
    {
        shuffleLines(false);
    }

    private void shuffleRows()
    {
        shuffleLines(true);
    }

    private void shuffleLines(bool isRow)
    {
        for (int i = 0; i < 3; i++)
        {
            int ranNum = random.Next(4);

            switch (ranNum)
            {
                case 0:
                    swapLines(0, 1, isRow, i);
                    break;
                case 1:
                    swapLines(1, 2, isRow, i);
                    break;
                case 2:
                    swapLines(0, 2, isRow, i);
                    break;

                default: return;
            }
        }
    }


    public void swapLines(int i1, int i2, bool isRow, int block)
    {
        if (!isValidLineSwapArg(i1, i2, isRow, block))
            throw new Exception(String.Format("Swap-arguments are not valid"));

        for (int i = 0; i < 9; i++)
        {
            int row1 = isRow ? i1 : i;
            int row2 = isRow ? i2 : i;
            int col1 = isRow ? i : i1;
            int col2 = isRow ? i : i2;

            int temp1 = getCell(row1, col1);
            int temp2 = getCell(row2, col2);
            setCell(row1, col1, temp2);
            setCell(row2, col2, temp1);
        }
    }


    private void shuffleColBlocks()
    {
        int ranNum = random.Next(4);

        switch (ranNum)
        {
            case 0:
                swapBlocks(0, 0, 0, 1);
                break;
            case 1:
                swapBlocks(0, 0, 0, 2);
                break;
            case 2:
                swapBlocks(0, 0, 1, 2);
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
    {
        if (!isValidBlockSwapArg(r1,r2,c1,c2))
            throw new Exception(String.Format("Swap-arguments are not valid"));

        int rowLimit = 3;
        int colLimit = 3;
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
            }
        }
    }

    public static bool isValidBlockSwapArg(int r1, int r2, int c1, int c2)
    {
        if (r1 < 0 || r2 < 0 || c1 < 0 || c2 < 0) return false;
        if (r1 >= 3 || r2 >= 3 || c1 >= 3 || c2 >= 3) return false;
        if (r1 != 0 || r2 != 0)
        {
            if (c1 != 0 || c2 != 0) return false;
        }
        return true;
    }

    public static bool isValidLineSwapArg(int i1, int i2, bool isRow, int block)
    {
        if (i1 < 0 || i2 < 0 || block < 0) return false;
        if (i1 >= 3 || i2 >= 3 || block >= 3) return false;
        //27
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

    public int getCell(int row, int col)
    {
        int index = row * 9 + col;
        return _matrix[index];
    }

    public int getCell(int index)
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
