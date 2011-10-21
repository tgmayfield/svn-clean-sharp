using System;
using System.IO;

namespace SvnCleanSharp.Core
{
	public class CleanFileInfo
		: ICleanFileInfo
	{
		public CleanFileInfo(FileInfo file, string relativePath)
		{
			_file = file;
			_relativePath = relativePath;
		}

		private readonly FileInfo _file;
		public FileInfo File
		{
			get { return _file; }
		}
		FileSystemInfo ICleanFileInfo.File
		{
			get { return File; }
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