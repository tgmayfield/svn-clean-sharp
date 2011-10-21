using System;

namespace SvnCleanSharp.ConsoleExtensions
{
	public class ConsoleColorDisposable
		: IDisposable
	{
		private readonly ConsoleColor _original;

		public ConsoleColorDisposable(ConsoleColor color)
		{
			_original = Console.ForegroundColor;
			Console.ForegroundColor = color;
		}

		public void Dispose()
		{
			Console.ForegroundColor = _original;
		}
	}
}