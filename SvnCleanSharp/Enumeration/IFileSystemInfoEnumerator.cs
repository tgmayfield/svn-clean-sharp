using System;
using System.Collections.Generic;
using System.IO;

using SvnCleanSharp.Core;

namespace SvnCleanSharp.Enumeration
{
	public interface ICleanFileInfoEnumerator
		: IEnumerable<ICleanFileInfo>
	{
		DirectoryInfo Root { get; }
	}
}