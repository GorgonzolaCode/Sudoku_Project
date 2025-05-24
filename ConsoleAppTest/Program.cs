using System;
using MathProblems;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string[] result = Generator.getProblem(Generator.Types.Addition);
            Console.WriteLine(result[0]);
            Console.WriteLine(result[1]);
        }
    }
}