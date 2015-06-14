using System;
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
	}
}