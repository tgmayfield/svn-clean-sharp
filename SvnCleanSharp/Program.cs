using System;
using System.Diagnostics;

using Plossum.CommandLine;

using SvnCleanSharp.Cleaning;
using SvnCleanSharp.ConsoleExtensions;
using SvnCleanSharp.Enumeration;

namespace SvnCleanSharp
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Inspired by http://stackoverflow.com/questions/239340/automatically-remove-subversion-unversioned-files/239371#239371
	/// </remarks>
	internal class Program
	{
		public static int Main()
		{
			try
			{
				return MainInside();
			}
			catch (Exception ex)
			{
				using (new ConsoleColorDisposable(ConsoleColor.Red))
				{
					Console.WriteLine(ex);
				}
				return -1;
			}
			finally
			{
				if (Debugger.IsAttached)
				{
					Console.WriteLine("-Complete-");
					Console.ReadKey();
				}
			}
		}

		private static int MainInside()
		{
			var options = new ProgramOptions();
			var parser = new CommandLineParser(options);
			parser.Parse();

			if (parser.RemainingArguments.Count == 0)
			{
				// No worries
			}
			else if (parser.RemainingArguments.Count == 1)
			{
				options.Directory = parser.RemainingArguments.First;
			}
			else
			{
				throw new InvalidOperationException(string.Format("Unknown arguments:\n\t{0}", string.Join("\n\t", parser.RemainingArguments)));
			}

			int width = Console.WindowWidth - 2;
			Console.WriteLine(parser.UsageInfo.GetHeaderAsString(width));

			if (options.ShowHelp)
			{
				Console.WriteLine(parser.UsageInfo.GetOptionsAsString(width));
				return 0;
			}
			if (parser.HasErrors)
			{
				using (new ConsoleColorDisposable(ConsoleColor.Red))
				{
					Console.WriteLine(parser.UsageInfo.GetErrorsAsString(width));
					return -1;
				}
			}

			if (string.IsNullOrEmpty(options.Directory))
			{
				options.Directory = Environment.CurrentDirectory;
			}

			ICleanFileInfoEnumerator enumerator = new IgnoredCleanFileInfoEnumerator(options.Directory);
			if (options.Exclude.Count > 0)
			{
				enumerator = enumerator.Excluding(options.Exclude);
			}

			ICleaner cleaner;
			if (options.DryRun)
			{
				cleaner = new DryRunCleaner();
			}
			else
			{
				cleaner = new ActualCleaner();
			}

			foreach (var file in enumerator)
			{
				cleaner.Clean(file);
			}

			return 0;
		}
	}
}