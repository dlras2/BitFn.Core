using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace BitFn.Core
{
	/// <summary>
	///     A class for getting a thread-static instance of <see cref="Random" />.
	/// </summary>
	/// <remarks>
	///     Adapted from grenade's answer on StackOverflow.
	///     http://stackoverflow.com/users/68115/
	///     http://stackoverflow.com/a/1262619/343238
	/// </remarks>
	public static class ThreadSafeRandom
	{
		[ThreadStatic] private static Random _local;

		/// <summary>
		///     Gets a thread-static instance of <see cref="Random" /> seeded by tick and thread id.
		/// </summary>
		public static Random Local
			=> _local ?? (_local = new Random(unchecked(Environment.TickCount*31 + Thread.CurrentThread.ManagedThreadId)));

		/// <summary>
		///     A delegate which returns a non-negative random integer. Uses a thread-static instance of <see cref="Random" />.
		/// </summary>
		/// <seealso cref="Random" />
		public static RandomInt RandomInt => () => Local.Next();

		/// <summary>
		///     A delegate which returns a non-negative random integer that is less than the specified maximum. Uses a
		///     thread-static instance of <see cref="Random" />.
		/// </summary>
		/// <seealso cref="Random" />
		public static RandomIntUnder RandomIntUnder => maxValue => Local.Next(maxValue);

		/// <summary>
		///     A delegate which returns a random integer that is within a specified range. Uses a thread-static instance of
		///     <see cref="Random" />.
		/// </summary>
		/// <seealso cref="Random" />
		public static RandomIntBetween RandomIntBetween => (minValue, maxValue) => Local.Next(minValue, maxValue);

		/// <summary>
		///     A delegate which returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
		///     Uses a thread-static instance of <see cref="Random" />.
		/// </summary>
		/// <seealso cref="Random" />
		public static RandomDouble RandomDouble => () => Local.NextDouble();

		/// <summary>
		///     A delegate which fills the elements of a specified array of bytes with random numbers. Uses a thread-static
		///     instance of <see cref="Random" />.
		/// </summary>
		/// <seealso cref="Random" />
		public static RandomBytes RandomBytes => buffer => Local.NextBytes(buffer);

		[ContractInvariantMethod]
		private static void ObjectInvariant()
		{
			Contract.Invariant(Local != null);
			Contract.Invariant(RandomInt != null);
			Contract.Invariant(RandomIntUnder != null);
			Contract.Invariant(RandomIntBetween != null);
			Contract.Invariant(RandomDouble != null);
			Contract.Invariant(RandomBytes != null);
		}
	}
}