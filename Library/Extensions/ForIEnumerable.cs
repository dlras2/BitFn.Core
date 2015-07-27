using System;
using System.Collections.Generic;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="IEnumerable{T}" /> interface.
	/// </summary>
	public static class ForIEnumerable
	{
		/// <summary>
		///     Orders the enumerable in a random order.
		/// </summary>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		/// <param name="source">The source to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			return source.Shuffle(ThreadSafeRandom.RandomIntBetween);
		}

		/// <summary>
		///     Orders the enumerable in a random order, using the provided random number generator.
		/// </summary>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		/// <param name="source">The source to use.</param>
		/// <param name="rng">The random number generator to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			if (rng == null) throw new ArgumentNullException(nameof(rng));

			return Shuffle(source, rng.Next);
		}

		/// <summary>
		///     Orders the enumerable in a random order, using the provided random number generator.
		/// </summary>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		/// <param name="source">The source to use.</param>
		/// <param name="rng">The random number generation function to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, RandomIntBetween rng)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (rng == null) throw new ArgumentNullException(nameof(rng));

			return ShuffleIterator(source, rng);
		}

		/// <remarks>
		///     Adapted from LukeH's answer on StackOverflow.
		///     http://stackoverflow.com/users/55847/
		///     http://stackoverflow.com/a/5807238/343238
		/// </remarks>
		private static IEnumerable<T> ShuffleIterator<T>(IEnumerable<T> source, RandomIntBetween rng)
		{
			var buffer = source.ToList();
			for (var i = 0; i < buffer.Count; i++)
			{
				var j = rng(i, buffer.Count);
				yield return buffer[j];

				buffer[j] = buffer[i];
			}
		}
	}
}