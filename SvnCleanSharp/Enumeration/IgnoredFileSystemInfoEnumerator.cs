using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using SvnCleanSharp.Core;

namespace SvnCleanSharp.Enumeration
{
	public class IgnoredCleanFileInfoEnumerator
		: ICleanFileInfoEnumerator
	{
		private readonly DirectoryInfo _directory;

		public IgnoredCleanFileInfoEnumerator(string directory)
			: this(new DirectoryInfo(directory))
		{
		}
		public IgnoredCleanFileInfoEnumerator(DirectoryInfo directory)
		{
			directory.Refresh();
			if (!directory.Exists)
			{
				throw new DirectoryNotFoundException(string.Format("Directory '{0}' does not exist", directory));
			}
			_directory = directory;
		}

		public DirectoryInfo Root
		{
			get { return _directory; }
		}

		public IEnumerable<ICleanFileInfo> GetFiles()
		{
			var startInfo = new ProcessStartInfo("svn.exe", "status --non-interactive --no-ignore")
			{
				UseShellExecute = false,
				RedirectStandardOutput = true,
				WorkingDirectory = _directory.FullName,
			};

			using (var process = Process.Start(startInfo))
			{
				bool hasLines = false;
				string line;
				while ((line = process.StandardOutput.ReadLine()) != null)
				{
					if (line.Length <= 7)
					{
						continue;
					}
					if (line[0] != '?')
					{
						continue;
					}
					hasLines = true;

					string relative = line.Substring(line.IndexOf(" ")).Trim();
					string path = Path.Combine(_directory.FullName, relative);
					if (Directory.Exists(path))
					{
						yield return new CleanDirectoryInfo(new DirectoryInfo(path), relative);
					}
					else if (File.Exists(path))
					{
						yield return new CleanFileInfo(new FileInfo(path), relative);
					}
					else
					{
						throw new Exception(string.Format("Subversion returned invalid path: {0}", path));
					}
				}

				if (!hasLines)
				{
					Console.WriteLine("No ignored/unversioned files found.");
				}
			}
		}

		public IEnumerator<ICleanFileInfo> GetEnumerator()
		{
			return GetFiles().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}