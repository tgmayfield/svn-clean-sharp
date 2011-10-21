using System;

using SvnCleanSharp.Core;

namespace SvnCleanSharp.Cleaning
{
	public interface ICleaner
	{
		void Clean(ICleanFileInfo file); 
	}
}