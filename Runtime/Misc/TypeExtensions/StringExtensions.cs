namespace Essentia
{
	public static class StringExtensions
	{
		public static string Remove(this string source, string substring)
		{
			return source.Replace(substring, string.Empty);
		}
	}
}
