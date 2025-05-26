using System;
using System.Numerics;

namespace Sudoku;

public interface GenInt
{
    int[] matrix { get;  set}

    public abstract string toString();
    public void print()
    {
        Console.WriteLine(toString());
    }
}
