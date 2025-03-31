using System.Reflection;

namespace Essentia.Reflection
{
    public static class Metadata
    {
		public const string NamespaceSeparationSymbol = ".";
		public const string EntryPointMethodName = "Main";
		public const BindingFlags EntryPointMethodBindingFlags = 
			BindingFlags.NonPublic | BindingFlags.Static;

		public const string GitKeepFileName = ".gitkeep";
	}
}
