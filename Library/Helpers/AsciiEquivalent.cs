namespace BitFn.Core.Helpers
{
	internal struct AsciiEquivalent
	{
		public AsciiEquivalent(string value)
		{
			Value = value;
		}

		public string Value { get; }

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
