namespace BitFn.Core
{
	/// <summary>
	///     A delegate which returns a non-negative random integer.
	/// </summary>
	/// <seealso cref="System.Random" />
	/// <returns>A non-negative random integer.</returns>
	public delegate int RandomInt();

	/// <summary>
	///     A delegate which returns a non-negative random integer that is less than the specified maximum.
	/// </summary>
	/// <seealso cref="System.Random" />
	/// <param name="maxValue">
	///     The exclusive upper bound of the random number to be generated. <paramref name="maxValue" />
	///     must be greater than or equal to 0.
	/// </param>
	/// <returns>A non-negative random integer that is less than the specified maximum.</returns>
	public delegate int RandomIntUnder(int maxValue);

	/// <summary>
	///     A delegate which returns a random integer that is within a specified range.
	/// </summary>
	/// <seealso cref="System.Random" />
	/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
	/// <param name="maxValue">
	///     The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be
	///     greater than or equal to <paramref name="minValue" />.
	/// </param>
	/// <returns>A random integer that is within a specified range.</returns>
	public delegate int RandomIntBetween(int minValue, int maxValue);

	/// <summary>
	///     A delegate which returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
	/// </summary>
	/// <seealso cref="System.Random" />
	/// <returns>A random floating-point number that is greater than or equal to 0.0, and less than 1.0.</returns>
	public delegate double RandomDouble();

	/// <summary>
	///     A delegate which fills the elements of a specified array of bytes with random numbers.
	/// </summary>
	/// <seealso cref="System.Random" />
	/// <param name="buffer">An array of bytes to contain random numbers.</param>
	public delegate void RandomBytes(byte[] buffer);
}