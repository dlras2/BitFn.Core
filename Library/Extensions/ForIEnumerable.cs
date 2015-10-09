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
		///     Aggregates the given sources into a single enumerable.
		/// </summary>
		/// <param name="source">The source to aggregate.</param>
		/// <returns>A single aggregated enumerable.</returns>
		public static IEnumerable<T> Aggregate<T>(this IEnumerable<IEnumerable<T>> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

			// ReSharper disable once PossibleMultipleEnumeration
			return source.SelectMany(_ => _);
		}

		/// <summary>
		///     Groups the elements of a sequence according to a specified key selector function, then counts the number of
		///     elements each group contains. Each element is counted exactly once.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> whose elements to count.</param>
		/// <param name="selector">A function to extract the key for each element.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains the selected key and number of elements with that key.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="selector" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="selector" /> returned a <c>null</c> key.</exception>
		public static IDictionary<TKey, int> CountBy<TValue, TKey>(
			this IEnumerable<TValue> source, Func<TValue, TKey> selector)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, int>>() != null);

			try
			{
				return source.GroupBy(selector)
					.Select(_ => new KeyValuePair<TKey, int>(_.Key, _.Count()))
					.ToDictionary();
			}
			catch (ArgumentNullException ex)
			{
				throw new ArgumentException($"{nameof(selector)} cannot return a null key.", ex);
			}
		}

		/// <summary>
		///     Groups the elements of a sequence according to a specified key selector function, compares the keys by using a
		///     specified comparer, then counts the number of elements each group contains. Each element is counted exactly once.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> whose elements to count.</param>
		/// <param name="selector">A function to extract the key for each element.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains the selected key and number of elements with that key.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="selector" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="selector" /> returned a <c>null</c> key.</exception>
		public static IDictionary<TKey, int> CountBy<TValue, TKey>(
			this IEnumerable<TValue> source, Func<TValue, TKey> selector, IEqualityComparer<TKey> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, int>>() != null);

			try
			{
				return source.GroupBy(selector, comparer)
					.Select(_ => new KeyValuePair<TKey, int>(_.Key, _.Count()))
					.ToDictionary();
			}
			catch (ArgumentNullException ex)
			{
				throw new ArgumentException($"{nameof(selector)} cannot return a null key.", ex);
			}
		}

		/// <summary>
		///     Groups the elements of a sequence according to a specified key selector function, then counts the number of
		///     elements each group contains. Each element is counted zero or more times.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> whose elements to count.</param>
		/// <param name="selector">A function to extract the keys for each element.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains the selected key and number of elements with that key.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="selector" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="selector" /> returned a <c>null</c> enumerable.</exception>
		public static IDictionary<TKey, int> CountByMany<TValue, TKey>(
			this IEnumerable<TValue> source, Func<TValue, IEnumerable<TKey>> selector)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, int>>() != null);

			try
			{
				return source.SelectMany(selector)
					.GroupBy(_ => _)
					.Select(_ => new KeyValuePair<TKey, int>(_.Key, _.Count()))
					.ToDictionary();
			}
			catch (NullReferenceException ex)
			{
				throw new ArgumentException($"{nameof(selector)} cannot return a null enumerable.", ex);
			}
		}

		/// <summary>
		///     Groups the elements of a sequence according to a specified key selector function, compares the keys by using a
		///     specified comparer, then counts the number of elements each group contains. Each element is counted zero or more
		///     times.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> whose elements to count.</param>
		/// <param name="selector">A function to extract the keys for each element.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains the selected key and number of elements with that key.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="selector" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="selector" /> returned a <c>null</c> enumerable.</exception>
		public static IDictionary<TKey, int> CountByMany<TValue, TKey>(
			this IEnumerable<TValue> source, Func<TValue, IEnumerable<TKey>> selector, IEqualityComparer<TKey> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(selector != null);
			Contract.Ensures(Contract.Result<IDictionary<TKey, int>>() != null);

			try
			{
				return source.SelectMany(selector)
					.GroupBy(_ => _, comparer)
					.Select(_ => new KeyValuePair<TKey, int>(_.Key, _.Count()))
					.ToDictionary();
			}
			catch (NullReferenceException ex)
			{
				throw new ArgumentException($"{nameof(selector)} cannot return a null enumerable.", ex);
			}
		}

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
		///     Iterates over each element of the enumerable, throwing the given exception if the predicate is matched.
		/// </summary>
		/// <param name="source">The sequence of elements to verify.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>The verified enumerable.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is <c>null</c>.</exception>
		[Pure]
		public static IEnumerable<TSource> ThrowIfAny<TSource, TException>(
			this IEnumerable<TSource> source, Func<TSource, bool> predicate)
			where TException : Exception, new()
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return ThrowIfAnyIterator(source, predicate, _ => new TException());
		}

		/// <summary>
		///     Iterates over each element of the enumerable, throwing the given exception if the predicate is matched.
		/// </summary>
		/// <param name="source">The sequence of elements to verify.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="exception">The exception to throw if the condition is met.</param>
		/// <returns>The verified enumerable.</returns>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="source" />, <paramref name="predicate" />, or <paramref name="exception" /> is <c>null</c>.
		/// </exception>
		[Pure]
		public static IEnumerable<TSource> ThrowIfAny<TSource, TException>(
			this IEnumerable<TSource> source, Func<TSource, bool> predicate, TException exception)
			where TException : Exception
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(exception != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return ThrowIfAnyIterator(source, predicate, _ => exception);
		}

		/// <summary>
		///     Iterates over each element of the enumerable, throwing the given exception if the predicate is matched.
		/// </summary>
		/// <param name="source">The sequence of elements to verify.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="exceptionFactory">A method returning the exception to throw if the condition is met.</param>
		/// <returns>The verified enumerable.</returns>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="source" />, <paramref name="predicate" />, or <paramref name="exceptionFactory" /> is <c>null</c>.
		/// </exception>
		[Pure]
		public static IEnumerable<TSource> ThrowIfAny<TSource, TException>(
			this IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TException> exceptionFactory)
			where TException : Exception
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Requires<ArgumentNullException>(predicate != null);
			Contract.Requires<ArgumentNullException>(exceptionFactory != null);
			Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

			return ThrowIfAnyIterator(source, predicate, exceptionFactory);
		}

		/// <summary>
		///     Creates a <see cref="Dictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="Dictionary{TKey,TValue}" /> from.</param>
		/// <returns>A <see cref="Dictionary{TKey,TValue}" /> that contains key value pairs selected from the input sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
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

		private static IEnumerable<TSource> ThrowIfAnyIterator<TSource, TException>(
			IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TException> exceptionFactory)
			where TException : Exception
		{
			foreach (var item in source)
			{
				if (predicate(item))
				{
					var exception = exceptionFactory(item);
					throw exception;
				}
				yield return item;
			}
		}
	}
}