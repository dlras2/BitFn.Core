using System.Globalization;

namespace BitFn.Core.Helpers
{
	internal struct AsciiEquivalent
	{
		public AsciiEquivalent(string value,
			UnicodeCategory? breakIfAfter = null,
			UnicodeCategory? breakIfBefore = null)
		{
			Value = value;
			BreakIfAfter = breakIfAfter;
			BreakIfBefore = breakIfBefore;
		}

		public string Value { get; }
		public UnicodeCategory? BreakIfAfter { get; }
		public UnicodeCategory? BreakIfBefore { get; }

		public static implicit operator AsciiEquivalent(string value)
		{
			return new AsciiEquivalent(value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
