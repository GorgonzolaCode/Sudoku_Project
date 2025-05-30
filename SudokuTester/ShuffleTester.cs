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
                        if (SudokuGenerator.isValidBlockSwapArg(row1, row2, col1, col2))
                        {
                            possibleCombinations++;
                        }
                    }
                }
            }
        }

        Assert.AreEqual(17, possibleCombinations);
    }

    [TestMethod]
    public void testLineSwapArgs()
    {
        int count = 0;

        for (int i1 = -1; i1 < 10; i1++)
        {
            for (int i2 = -1; i2 < 10; i2++)
            {
                for (int block = -1; block < 4; block++)
                {
                    if (SudokuGenerator.isValidLineSwapArg(i1, i2, true, block)) count++;
                    if (SudokuGenerator.isValidLineSwapArg(i1, i2, false, block)) count++;
                }
            }
        }

        Assert.AreEqual(54, count);
    }

    [TestMethod]
    public void testLineSwapIdentical()
    {
        for (int block = 0; block < 3; block++)
        {
            for (int i = 0; i < 3; i++)
            {
                sudoku2.swapLines(i, i, true, block);
                Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
                sudoku2.swapLines(i, i, false, block);
                Assert.AreEqual(sudoku1.ToString(), sudoku2.ToString());
            }
        }
    }

    [TestMethod]
    public void testLineSwap()
    {
        sudoku2.swapLines(0, 1, true, 0);
        string expected = """
            -------------------------------
            | 4  5  6 | 7  8  6 | 1  2  3 |
            | 1  2  3 | 4  5  6 | 7  8  3 |
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
            """.Trim();
        Assert.AreEqual(expected, sudoku2.ToString().Trim());

        sudoku2.swapLines(0, 1, false, 0);
        expected = """
            -------------------------------
            | 5  4  6 | 7  8  6 | 1  2  3 |
            | 2  1  3 | 4  5  6 | 7  8  3 |
            | 8  7  9 | 1  2  3 | 4  5  6 |
            -------------------------------
            | 3  2  4 | 5  6  7 | 8  9  1 |
            | 6  5  7 | 8  9  1 | 2  3  4 |
            | 8  8  1 | 2  3  4 | 5  6  7 |
            -------------------------------
            | 4  3  5 | 6  7  8 | 9  1  2 |
            | 7  6  8 | 9  1  2 | 3  4  5 |
            | 1  1  2 | 3  4  5 | 6  7  8 |
            -------------------------------
            """.Trim();
        Assert.AreEqual(expected, sudoku2.ToString().Trim());

        sudoku2.swapLines(0, 1, false, 1);
        sudoku2.swapLines(0, 1, true, 1);
        sudoku2.swapLines(1, 2, false, 2);
        sudoku2.swapLines(2, 1, true, 2);
        expected = """
            -------------------------------
            | 1  3  2 | 4  5  6 | 7  8  3 |
            | 7  8  8 | 1  2  3 | 4  5  6 |
            | 4  6  5 | 7  8  6 | 1  2  3 |
            -------------------------------
            | 2  4  3 | 5  6  7 | 8  9  1 |
            | 5  7  6 | 8  9  1 | 2  3  4 |
            | 8  1  8 | 2  3  4 | 5  6  7 |
            -------------------------------
            | 3  5  4 | 6  7  8 | 9  1  2 |
            | 6  8  7 | 9  1  2 | 3  4  5 |
            | 1  2  1 | 3  4  5 | 6  7  8 |
            -------------------------------
            """.Trim();
        Assert.AreEqual(expected, sudoku2.ToString().Trim());
    }

    [TestMethod]
    public void testValidity()
    {
        int value = sudoku2.getCell(0, 0);
        sudoku2.setCell(0, 0, value % 9 + 1);
        bool validity = sudoku2.colsValid();
        Assert.IsFalse(validity);
        validity = sudoku2.rowsValid();
        Assert.IsFalse(validity);
        validity = sudoku2.blocksValid();
        Assert.IsFalse(validity);
        sudoku2.setCell(0, 0, value);
        Assert.IsTrue(sudoku2.isValid());
    }



}



