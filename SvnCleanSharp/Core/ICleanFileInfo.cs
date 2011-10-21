using System;
using System.IO;

namespace SvnCleanSharp.Core
{
	public interface ICleanFileInfo
	{
		FileSystemInfo File { get; }
		string RelativePath { get; }
	}
}