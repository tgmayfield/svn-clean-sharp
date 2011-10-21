using System;

using SvnCleanSharp.Core;

namespace SvnCleanSharp.Cleaning
{
	public class DryRunCleaner
		: CleanerBase
	{
		protected override void Clean(CleanDirectoryInfo directory)
		{
			Console.WriteLine("(NOT) Deleting directory: {0}", directory.RelativePath);
		}

		protected override void Clean(CleanFileInfo file)
		{
			Console.WriteLine("(NOT) Deleting file:      {0}", file.RelativePath);
		}
	}
}