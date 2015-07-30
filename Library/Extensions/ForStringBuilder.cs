using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods extending <see cref="StringBuilder" />.
	/// </summary>
	public static class ForStringBuilder
	{
		/// <summary>
		///     Appends a specified number of copies of a string to this instance.
		/// </summary>
		/// <param name="sb">The <see cref="StringBuilder" /> to append to.</param>
		/// <param name="value">The string to append.</param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />.</param>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="sb" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="repeatCount" /> is less than <c>0</c>.</exception>
		public static StringBuilder Append(this StringBuilder sb, string value, int repeatCount)
		{
			Contract.Requires<ArgumentNullException>(sb != null);
			Contract.Requires<ArgumentOutOfRangeException>(repeatCount >= 0);
			Contract.Ensures(Contract.Result<StringBuilder>() != null);

			if (repeatCount == 0 || string.IsNullOrEmpty(value))
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