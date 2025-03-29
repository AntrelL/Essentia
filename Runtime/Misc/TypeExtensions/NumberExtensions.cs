using System;

namespace Essentia
{
	public static class NumberExtensions
	{
		private const string InvalidRangeError =
			"The max number of the range must be greater than the min.";

		public static bool IsInRange(this int number, int min, int max) =>
			IsInRange<int>(number, min, max);

		public static bool IsInRange(this float number, float min, float max) =>
			IsInRange<float>(number, min, max);

		private static bool IsInRange<T>(T number, T min, T max) where T : IComparable<T>
		{
			if (min.CompareTo(max) >= 0)
			{
				Console.LogError(InvalidRangeError, typeName: nameof(NumberExtensions));
				return false;
			}

			return number.CompareTo(min) >= 0 && number.CompareTo(max) <= 0;
		}
	}
}
