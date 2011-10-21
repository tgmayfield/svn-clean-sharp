using System;
using System.Collections.Generic;

using Plossum.CommandLine;

namespace SvnCleanSharp
{
	[CommandLineManager(
		ApplicationName = "SVN Clean#", 
		Copyright = " ",
		EnabledOptionStyles = OptionStyles.LongUnix | OptionStyles.ShortUnix, 
		IsCaseSensitive = false)]
	public class ProgramOptions
	{
		public ProgramOptions()
		{
			Exclude = new List<string>();
		}

		[CommandLineOption(Description = "Displays this help text",
			Aliases = "help")]
		public bool ShowHelp { get; set; }

		[CommandLineOption(Description = "Directory to clean. Uses current directory if not specified.")]
		public string Directory { get; set; }
		
		[CommandLineOption(Description = "Regular expressions to exclude files from the match. May be specified numerous times.")]
		public List<string> Exclude { get; private set; }
		
		[CommandLineOption(Description = "When specified, no changes will be made. They will just be listed.",
			Aliases = "dry-run",
			BoolFunction = BoolFunction.TrueIfPresent)]
		public bool DryRun { get; set; }
	}
}