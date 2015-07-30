using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="IEnumerable{T}" /> interface.
	/// </summary>
	public static class ForIEnumerable
	{
		/// <summary>
		///     Sorts the elements of a sequence in ascending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedEnumerable<T>>() != null);

			return source.OrderBy(_ => _);
		}

		/// <summary>
		///     Sorts the elements of a sequence in ascending order according to themselves by using a specified comparer.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare elements.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source, IComparer<T> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedEnumerable<T>>() != null);

			return source.OrderBy(_ => _, comparer);
		}

		/// <summary>
		///     Sorts the elements of a sequence in descending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedEnumerable<T>>() != null);

			return source.OrderByDescending(_ => _);
		}

		/// <summary>
		///     Sorts the elements of a sequence in descending order according to themselves by using a specified comparer.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="comparer">An <see cref="IComparer{T}" /> to compare elements.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source, IComparer<T> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedEnumerable<T>>() != null);

			return source.OrderByDescending(_ => _, comparer);
		}

		/// <summary>
		///     Orders the enumerable in a random order.
		/// </summary>
		/// <param name="source">The source to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		[Pure]
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			return source.Shuffle(ThreadSafeRandom.RandomIntBetween);
		}

		/// <summary>
		///     Orders the enumerable in a random order, using the provided random number generator.
		/// </summary>
		/// <param name="source">The source to use.</param>
		/// <param name="rng">The random number generator to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="rng" /> is <c>null</c>.</exception>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		[Pure]
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(rng != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			return Shuffle(source, rng.Next);
		}

		/// <summary>
		///     Orders the enumerable in a random order, using the provided random number generator.
		/// </summary>
		/// <param name="source">The source to use.</param>
		/// <param name="rng">The random number generation function to use.</param>
		/// <returns>The enumerable in a random order.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="rng" /> is <c>null</c>.</exception>
		/// <remarks>
		///     The resulting enumerable forces execution of the source once any consumption begins.
		/// </remarks>
		[Pure]
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, RandomIntBetween rng)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(rng != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			return ShuffleIterator(source, rng);
		}

		/// <summary>
		///     Creates a <see cref="Dictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="Dictionary{TKey,TValue}" /> from.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains key value pairs selected from the input sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, TValue>>() != null);

			return source.ToDictionary(_ => _.Key, _ => _.Value);
		}

		/// <summary>
		///     Creates a <see cref="Dictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" /> according to a specified
		///     comparer.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="Dictionary{TKey,TValue}" /> from.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains key value pairs selected from the input sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(
			this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, TValue>>() != null);

			return source.ToDictionary(_ => _.Key, _ => _.Value, comparer);
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