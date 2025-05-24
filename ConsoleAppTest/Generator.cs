using System;

namespace MathProblems;




public class Generator
{
	[Flags]
	public enum Types {
	Addition,
	Multiplication,
	Division
}

	private static Random randy = new Random();


	public static string[] getProblem(Types types)
	{
		//problem / solution
		string[] result = { "", "" };
		float solution = 0;

		if ((types & Types.Addition) == Types.Addition)
		{
			int a = randy.Next(10);
			int b = randy.Next(10);
			result[0] += a + "+" + b;
			solution = a + b;
		}


		result[1] = solution.ToString();
		return result;
	}

}
