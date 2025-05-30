using Sudoku;

namespace SudokuTester;

[TestClass]
public sealed class ShuffleTester
{
    SudokuGenerator? sudoku1;
    SudokuGenerator? sudoku2;


    [TestInitialize]
    public void setup()
    {
        sudoku1 = new SudokuGenerator();
        sudoku1.setBoard();
        sudoku2 = new SudokuGenerator();
        sudoku2.setBoard();
    }


    [TestMethod]
    public void testDefault()
    {
        string actual = sudoku1.ToString();
        string expected = """
        -------------------------------
        | 1  2  3 | 4  5  6 | 7  8  9 |
        | 4  5  6 | 7  8  9 | 1  2  3 |
        | 7  8  9 | 1  2  3 | 4  5  6 |
        -------------------------------
        | 2  3  4 | 5  6  7 | 8  9  1 |
        | 5  6  7 | 8  9  1 | 2  3  4 |
        | 8  9  1 | 2  3  4 | 5  6  7 |
        -------------------------------
        | 3  4  5 | 6  7  8 | 9  1  2 |
        | 6  7  8 | 9  1  2 | 3  4  5 |
        | 9  1  2 | 3  4  5 | 6  7  8 |
        -------------------------------
        """;

        Console.WriteLine(actual.Trim());
        Console.WriteLine("");
        Console.WriteLine(expected.Trim());

        Assert.AreEqual(expected.Trim(), actual.Trim());
    }

    [TestMethod]
    public void testBlockSwapCol()
    {
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        sudoku1.swapColBlocks(0, 1);
        sudoku2.swapBlocks(0, 0, 0, 1);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        (sudoku1 as SudokuInterface).print();
        sudoku1.swapColBlocks(0, 1);
        sudoku2.swapBlocks(0, 0, 1, 0);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        (sudoku1 as SudokuInterface).print();
        sudoku2.swapBlocks(0, 0, 1, 1);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        (sudoku1 as SudokuInterface).print();
        sudoku2.swapBlocks(0, 0, 0, 0);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        (sudoku1 as SudokuInterface).print();
        sudoku2.swapBlocks(0, 0, 2, 2);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        (sudoku1 as SudokuInterface).print();
    }

    [TestMethod]
    public void testBlockSwapRowIdentical()
    {
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        sudoku2.swapBlocks(1, 1, 0, 0);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
        sudoku2.swapBlocks(2, 2, 0, 0);
        Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
    }

    [TestMethod]
    public void testBlockSwapArgs()
    {
        int possibleCombinations = 0;

        for (int col1 = -1; col1 < 4; col1++)
        {
            for (int col2 = -1; col2 < 4; col2++)
            {
                for (int row1 = -1; row1 < 4; row1++)
                {
                    for (int row2 = -1; row2 < 4; row2++)
                    {
                        if (SudokuGenerator.isValidSwapArg(row1, row2, col1, col2))
                        {
                            possibleCombinations++;
                        }
                    }
                }
            }
        }

        Assert.AreEqual(17, possibleCombinations);
    }


}



