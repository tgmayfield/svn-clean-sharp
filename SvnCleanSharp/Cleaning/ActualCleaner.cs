using System;

using SvnCleanSharp.ConsoleExtensions;
using SvnCleanSharp.Core;

namespace SvnCleanSharp.Cleaning
{
	public class ActualCleaner
		: CleanerBase
	{
		protected override void Clean(CleanDirectoryInfo directory)
		{
			Console.WriteLine("Deleting directory: {0}", directory.RelativePath);
			try
			{
				directory.Directory.Delete(true);
			}
			catch (Exception ex)
			{
				using (new ConsoleColorDisposable(ConsoleColor.Red))
				{
					Console.WriteLine("\tFailed: {0}", ex.Message);
				}
			}
		}

		protected override void Clean(CleanFileInfo file)
		{
			Console.WriteLine("Deleting file:      {0}", file.RelativePath);
			try
			{
				file.File.Delete();
			}
			catch (Exception ex)
			{
				using (new ConsoleColorDisposable(ConsoleColor.Red))
				{
					Console.WriteLine("\tFailed: {0}", ex.Message);
				}
			}
		}
	}
}