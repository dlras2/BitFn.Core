using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="char" /> class.
	/// </summary>
	public static class ForChar
	{
		/// <summary>
		///     Converts the Unicode value of this character to its equivalent 4-character hexadecimal string representation.
		/// </summary>
		/// <param name="ch">A character to convert to a hexadecimal string.</param>
		/// <returns>The equivalent 4-character hexadecimal string representation.</returns>
		[Pure]
		public static string ToHex(this char ch)
		{
			Contract.Ensures(Contract.Result<string>() != null);
			Contract.Ensures(Contract.Result<string>().Length == 4);

			return string.Join(string.Empty, BitConverter.GetBytes(ch).Reverse().Select(_ => $"{_,2:X}"))
				.Replace(' ', '0');
		}
	}
}