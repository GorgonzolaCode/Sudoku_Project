using System;

namespace MathProblems;

public interface GeneratorInterface
{
    [Flags]
	public enum Types
	{
		Addition,
		Multiplication,
		Division
	}


    static abstract string[] getProblem(Types types);

}
