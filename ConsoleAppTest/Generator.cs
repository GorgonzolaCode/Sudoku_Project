using System;

namespace MathProblems;

public class Generator:GeneratorInterface
{
	private static Random randy = new Random();

    public static string[] getProblem(ProblemTypes types)
	{
		//problem / solution
		string[] result = { "", "" };
		float solution = 0;

		if ((types & ProblemTypes.Addition) == ProblemTypes.Addition)
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




