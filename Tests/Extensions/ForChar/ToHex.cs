using NUnit.Framework;

// ReSharper disable InvokeAsExtensionMethod

namespace BitFn.Core.Tests.Extensions.ForChar
{
	[TestFixture]
	public class ToHex
	{
		[TestCase('A', Result = "0041")]
		[TestCase('a', Result = "0061")]
		[TestCase('Æ', Result = "00C6")]
		[TestCase('æ', Result = "00E6")]
		[TestCase('\u0123', Result = "0123")]
		[TestCase('\uDEAD', Result = "DEAD")]
		[TestCase('\uBEEF', Result = "BEEF")]
		public string ResultFor_Character(char ch)
		{
			return Core.Extensions.ForChar.ToHex(ch);
		}
	}
}