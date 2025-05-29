using Sudoku;

namespace SudokuTester;

[TestClass]
public sealed class ShuffleTester
{
    [TestMethod]
    public void testDefault()
    {
        SudokuInterface sudoku = new SudokuGenerator();

        sudoku.generateBoard();
        string actual = sudoku.ToString();
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
}



