using System;
using System.IO;

namespace SvnCleanSharp.Core
{
	public class CleanDirectoryInfo
		: ICleanFileInfo
	{
		public CleanDirectoryInfo(DirectoryInfo directory, string relativePath)
		{
			_directory = directory;
			_relativePath = relativePath;
		}

		private readonly DirectoryInfo _directory;
		public DirectoryInfo Directory
		{
			get { return _directory; }
		}
		FileSystemInfo ICleanFileInfo.File
		{
			get { return Directory; }
		}

		private readonly string _relativePath;
		public string RelativePath
		{
			get { return _relativePath; }
		}

		public override string ToString()
		{
			return RelativePath;
		}
	}
}