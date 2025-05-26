using System;
using System.Runtime.InteropServices;

namespace Sudoku;

public class Generator : GenInt
{
    private int[] _matrix = new int[81];
    public int[] matrix { get => _matrix; set => _matrix = value; }

    public string toString()
    {
        return "Hello";
    }
}
