﻿// See https://aka.ms/new-console-template for more information
using Sudoku;

Console.WriteLine("Hello, World!");


SudokuInterface gen = new SudokuGenerator();
gen.generateBoard();
gen.print();