using System;
using System.IO;

using SvnCleanSharp.Core;

namespace SvnCleanSharp.Cleaning
{
	public abstract class CleanerBase
		: ICleaner
	{
		public void Clean(ICleanFileInfo file)
		{
			if (file is CleanDirectoryInfo)
			{
				Clean(file as CleanDirectoryInfo);
			}
			else if (file is CleanFileInfo)
			{
				Clean(file as CleanFileInfo);
			}
			else
			{
				throw new ArgumentOutOfRangeException("file", string.Format("Unknown ICleanFileInfo type {0} for {1}", file.GetType(), file));
			}
		}

		protected abstract void Clean(CleanDirectoryInfo directory);

		protected abstract void Clean(CleanFileInfo file);
	}
}