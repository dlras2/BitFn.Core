using System;
using System.Text;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods extending <see cref="StringBuilder" />.
	/// </summary>
	public static class ForStringBuilder
	{
		/// <summary>
		///     Appends the specified number of copies of the string to this instance.
		/// </summary>
		/// <param name="sb">The string builder to append to.</param>
		/// <param name="value">The value to append numerous times.</param>
		/// <param name="repeatCount">The number of times to append the string.</param>
		/// <returns>The string builder instance.</returns>
		public static StringBuilder Append(this StringBuilder sb, string value, int repeatCount)
		{
			if (sb == null) throw new ArgumentNullException(nameof(sb));

			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(repeatCount), repeatCount, "Argument cannot be less than zero.");
			}
			if (repeatCount == 0 || value == null || value.Length == 0)
			{
				return sb;
			}

			sb.EnsureCapacity(sb.Length + value.Length*repeatCount);
			for (var i = 0; i < repeatCount; i++)
			{
				sb.Append(value);
			}
			return sb;
		}
	}
}