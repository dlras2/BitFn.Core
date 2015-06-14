using System;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="char" /> class.
	/// </summary>
	public static class ForChar
	{
		/// <summary>
		///     Returns the 4-digit hexadecimal string representing the given Unicode character.
		/// </summary>
		/// <param name="ch">The character to use.</param>
		/// <returns>The 4-digit hexadecimal string representing the given Unicode character.</returns>
		public static string ToHex(this char ch)
		{
			return string.Join(string.Empty, BitConverter.GetBytes(ch).Reverse().Select(_ => $"{_,2:X}"))
				.Replace(' ', '0');
		}
	}
}