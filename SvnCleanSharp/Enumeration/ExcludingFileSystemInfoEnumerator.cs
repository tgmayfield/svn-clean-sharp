using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using SvnCleanSharp.ConsoleExtensions;
using SvnCleanSharp.Core;

namespace SvnCleanSharp.Enumeration
{
	public class ExcludingCleanFileInfoEnumerator
		: ICleanFileInfoEnumerator
	{
		private readonly ICleanFileInfoEnumerator _inner;
		private readonly string _exclude;
		private readonly Regex _matcher;

		public ExcludingCleanFileInfoEnumerator(ICleanFileInfoEnumerator inner, string exclude)
		{
			_inner = inner;
			_exclude = exclude;
			_matcher = new Regex(exclude, RegexOptions.IgnoreCase);
		}

		public DirectoryInfo Root
		{
			get { return _inner.Root; }
		}
		public IEnumerable<ICleanFileInfo> GetFiles()
		{
			foreach (var file in _inner)
			{
				if (_matcher.IsMatch(file.RelativePath))
				{
					using (new ConsoleColorDisposable(ConsoleColor.Yellow))
					{
						Console.WriteLine("Skipped: {0} (matched '{1}')", file.RelativePath, _exclude);
					}
					continue;
				}

				yield return file;
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