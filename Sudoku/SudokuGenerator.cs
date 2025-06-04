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



    public void shuffle()
    {
        swapPartials();
        shuffleCols();
        shuffleColBlocks();
        shuffleRows();
        shuffleRowBlocks();
        swapNumbers();
    }

    public void swapPartials()
    {
        // TODO unify methods into one
        swapPartialColsInTriplets();
        swapPartialRowsInTriplets();

        //TODO swap partials in pairs
    }

    #region partial row swap in three pairs

    /**
        swaps three pairs of numbers
        (1,2)
        (2,3)
        (3,1)
        horizontally
    **/
    private void swapPartialRowsInTriplets()
    {
        for (int colBlock = 0; colBlock < 3; colBlock++)
        {
            for (int row = 0; row < 3; row++)
            {
                swapPartialRowsInTripletsV(row, colBlock);
            }
        }
    }
    
    private void swapPartialRowsInTripletsV(int row, int colBlock)
    {
        if (random.Next(2) == 1)
            swapPartialRowsInTripletsV(0, 1, row, colBlock);
        if (random.Next(2) == 1)
            swapPartialRowsInTripletsV(0, 2, row, colBlock);
        if (random.Next(2) == 1)
            swapPartialRowsInTripletsV(1, 2, row, colBlock);
    }

	private void swapPartialRowsInTripletsV(int col1, int col2, int inRow, int colBlock)
	{
		int[]? positions = findForPRITV(col1, col2, inRow, colBlock);
		if (positions is null) return;

		foreach (int row in positions)
		{
			int temp1 = getCell(row, col1 + 3 * colBlock);
			int temp2 = getCell(row, col2 + 3 * colBlock);
			setCell(row, col1 + 3 * colBlock, temp2);
			setCell(row, col2 + 3 * colBlock, temp1);
		}
	}

    private int[]? findForPRITV(int col1, int col2, int row, int colBlock)
    {
        int[]? result = new int[3];
        result[0] = row;

        col1 += 3 * colBlock;
        col2 += 3 * colBlock;
        int toFind = getCell(row, col2);

        for (int r = 3; r < 9; r++)
        {
            int curVal = getCell(r, col1);
            if (curVal == toFind)
            {
                result[1] = r;
                break;
            }
        }
        int rowBlock = result[1] / 3;
        rowBlock = rowBlock == 2 ? 1 : 2;
		toFind = getCell(result[1], col2);

		for (int r = 3 * rowBlock; r < 3 * rowBlock + 3; r++)
		{
			int curVal = getCell(r, col1);
			if (curVal == toFind)
			{
				result[2] = r;
				break;
			}
		}
		//check if triplet was found
		int firstValue = getCell(result[0], col1);
		int lastValue = getCell(result[2], col2);
		if (result[2] == 0 || firstValue != lastValue) return null;

		return result;
    }




    /**
        swaps three pairs of numbers
        (1,2)
        (2,3)
        (3,1)
        vertically
    **/
    private void swapPartialColsInTriplets()
    {
        for (int rowBlock = 0; rowBlock < 3; rowBlock++)
        {
            for (int col = 0; col < 3; col++)
            {
                swapPartialColsInTripletsV(col, rowBlock);
            }
        }
    }

    private void swapPartialColsInTripletsV(int col, int rowBlock)
    {
        if (random.Next(2)==1)
            swapPartialColsInTripletsV(0, 1, col, rowBlock);
        if (random.Next(2)==1)
            swapPartialColsInTripletsV(0, 2, col, rowBlock);
        if (random.Next(2)==1)
            swapPartialColsInTripletsV(1, 2, col, rowBlock);
    }

    private void swapPartialColsInTripletsV(int row1, int row2, int inCol, int rowBlock)
    {
        int[]? positions = findForPCITV(row1, row2, inCol, rowBlock);
		if (positions is null) return;

		foreach (int col in positions)
		{
			int temp1 = getCell(row1 + 3 * rowBlock, col);
			int temp2 = getCell(row2 + 3 * rowBlock, col);
			setCell(row1 + 3 * rowBlock, col, temp2);
			setCell(row2 + 3 * rowBlock, col, temp1);
		}
    }

    private int[]? findForPCITV(int row1, int row2, int col, int rowBlock)
    {
        int[]? result = new int[3];
        result[0] = col;

        row1 += 3 * rowBlock;
        row2 += 3 * rowBlock;
        int toFind = getCell(row2, col);

        for (int c = 3; c < 9; c++)
        {
            int curVal = getCell(row1, c);
            if (curVal == toFind)
            {
                result[1] = c;
                break;
            }
        }
        int colBlock = result[1] / 3;
        colBlock = colBlock == 2 ? 1 : 2;
		toFind = getCell(row2, result[1]);

		for (int c = 3 * colBlock; c < 3 * colBlock + 3; c++)
		{
			int curVal = getCell(row1, c);
			if (curVal == toFind)
			{
				result[2] = c;
				break;
			}
		}
		//check if triplet was found
		int firstValue = getCell(row1, result[0]);
		int lastValue = getCell(row2, result[2]);
		if (result[2] == 0 || firstValue != lastValue) return null;

		return result;
    }


    #endregion


    //swaps nubers around globally 
    public void swapNumbers()
    {
        int[] newNums = getNewNumbers();

        for (int i = 0; i < 81; i++)
        {
            int value = getCell(i);
            setCell(i, newNums[value - 1]);
        }
    }

    //generates the numbers 1-9 in random order
    private int[] getNewNumbers()
    {
        int[] numbers = new int[9];

        for (int i = 0; i < 9; i++)
        {
            numbers[i] = i + 1;
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                bool doSwap = random.Next(2) == 1;
                if (doSwap)
                {
                    int temp1 = numbers[i];
                    int temp2 = numbers[j];
                    numbers[i] = temp2;
                    numbers[j] = temp1;
                }
            }
        }

        return numbers;
    }

    //swaps around columns in each block
    private void shuffleCols()
    {
        shuffleLines(false);
    }

    //swaps around rows in each block
    private void shuffleRows()
    {
        shuffleLines(true);
    }

    //decides, what lines get swapped and does it
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

    //swaps content of two specified lines
    public void swapLines(int index1, int index2, bool isRow, int blockIndex)
    {
        if (!isValidLineSwapArg(index1, index2, isRow, blockIndex))
            throw new Exception(String.Format("Swap-arguments are not valid"));

        for (int i = 0; i < 9; i++)
        {
            int row1 = isRow ? index1 + 3 * blockIndex : i;
            int row2 = isRow ? index2 + 3 * blockIndex : i;
            int col1 = isRow ? i : index1 + 3 * blockIndex;
            int col2 = isRow ? i : index2 + 3 * blockIndex;

            int temp1 = getCell(row1, col1);
            int temp2 = getCell(row2, col2);
            setCell(row1, col1, temp2);
            setCell(row2, col2, temp1);
        }
    }


    //decides, what block columns get swapped and does it
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

    //decides, what block rows get swapped and does it
    private void shuffleRowBlocks()
    {
        int ranNum = random.Next(4);

        switch (ranNum)
        {
            case 0:
                swapBlocks(0, 1, 0, 0);
                break;
            case 1:
                swapBlocks(0, 2, 0, 0);
                break;
            case 2:
                swapBlocks(1, 2, 0, 0);
                break;

            default: return;
        }

    }

    //swaps two block rows/columns
    public void swapBlocks(int rowIndex1, int rowIndex2, int colIndex1, int colIndex2)
    {
        if (!isValidBlockSwapArg(rowIndex1,rowIndex2,colIndex1,colIndex2))
            throw new Exception(String.Format("Swap-arguments are not valid"));
        if (rowIndex1 == rowIndex2 && colIndex1 == colIndex2) return;

        int rowLimit = (rowIndex1 == rowIndex2 && rowIndex1 == 0)? 9 : 3;
        int colLimit = (colIndex1 == colIndex2 && colIndex1 == 0)? 9 : 3;

        for (int row = 0; row < rowLimit; row++)
        {
            for (int col = 0; col < colLimit; col++)
            {
                int temp1 = getCell(row + 3 * rowIndex1, col + 3 * colIndex1);
                int temp2 = getCell(row + 3 * rowIndex2, col + 3 * colIndex2);
                setCell(row + 3 * rowIndex1, col + 3 * colIndex1, temp2);
                setCell(row + 3 * rowIndex2, col + 3 * colIndex2, temp1);
            }
        }
    }

    //checks for valid aruments
    public static bool isValidBlockSwapArg(int rowIndex1, int rowIndex2, int colIndex1, int colIndex2)
    {
        if (rowIndex1 < 0 || rowIndex2 < 0 || colIndex1 < 0 || colIndex2 < 0) return false;
        if (rowIndex1 >= 3 || rowIndex2 >= 3 || colIndex1 >= 3 || colIndex2 >= 3) return false;
        if (rowIndex1 != 0 || rowIndex2 != 0)
        {
            if (colIndex1 != 0 || colIndex2 != 0) return false;
        }
        return true;
    }

    //checks for valid aruments
    public static bool isValidLineSwapArg(int index1, int index2, bool isRow, int block)
    {
        if (index1 < 0 || index2 < 0 || block < 0) return false;
        if (index1 >= 3 || index2 >= 3 || block >= 3) return false;

        return true;
    }

    //inserts value if applicable
    public void setCell(int row, int col, int value)
    {
        if (row < 9 && col < 9 && value <= 9 && row >= 0 && col >= 0 && value >= 0)
        {
            int index = row * 9 + col;
            _matrix[index] = value;
        }
    }

    //inserts value if applicable
    public void setCell(int index, int value)
    {
        if (index >= 0 && index < 81 && value <= 9 && value >= 0)
        {
            _matrix[index] = value;
        }
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
