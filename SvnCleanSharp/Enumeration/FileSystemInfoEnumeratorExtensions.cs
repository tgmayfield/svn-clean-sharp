using System;
using System.Collections.Generic;
using System.Linq;

namespace SvnCleanSharp.Enumeration
{
	public static class CleanFileInfoEnumeratorExtensions
	{
		public static ICleanFileInfoEnumerator Excluding(this ICleanFileInfoEnumerator enumerator, params string[] regularExpressions)
		{
			return enumerator.Excluding(regularExpressions.AsEnumerable());
		}
		public static ICleanFileInfoEnumerator Excluding(this ICleanFileInfoEnumerator enumerator, IEnumerable<string> regularExpressions)
		{
			foreach (string expression in regularExpressions)
			{
				enumerator = new ExcludingCleanFileInfoEnumerator(enumerator, expression);
			}
			return enumerator;
		}
	}
}