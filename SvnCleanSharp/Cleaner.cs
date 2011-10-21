using System;
using System.Diagnostics;
using System.IO;

namespace SvnCleanSharp
{
	public class Cleaner
	{
		public void Run()
		{
			string directory = @"C:\Temp";
			Console.WriteLine("SVN cleaning directory {0}", directory);

			Directory.SetCurrentDirectory(directory);

			var psi = new ProcessStartInfo("svn.exe", "status --non-interactive");
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;
			psi.WorkingDirectory = directory;

			using (var process = Process.Start(psi))
			{
				string line = process.StandardOutput.ReadLine();
				while (line != null)
				{
					if (line.Length > 7)
					{
						if (line[0] == '?')
						{
							string relativePath = line.Substring(7);
							Console.WriteLine(relativePath);

							string path = Path.Combine(directory, relativePath);
							if (Directory.Exists(path))
							{
								Directory.Delete(path, true);
							}
							else if (File.Exists(path))
							{
								File.Delete(path);
							}
						}
					}
					line = process.StandardOutput.ReadLine();
				}
			}
		}
	}
}