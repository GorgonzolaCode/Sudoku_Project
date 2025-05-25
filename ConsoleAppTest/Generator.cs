using System;

namespace MathProblems;

public class Generator:GeneratorInterface
{
	private static Random randy = new Random();

    public static string[] getProblem(GeneratorInterface.Types types)
	{
		//problem / solution
		string[] result = { "", "" };
		float solution = 0;

		if ((types & GeneratorInterface.Types.Addition) == GeneratorInterface.Types.Addition)
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




