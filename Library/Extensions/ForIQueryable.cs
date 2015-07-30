using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="IQueryable{T}" /> interface.
	/// </summary>
	public static class ForIQueryable
	{
		/// <summary>
		///     Sorts the elements of a sequence in ascending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedQueryable<T>>() != null);

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
		public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, IComparer<T> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedQueryable<T>>() != null);

			// TODO: Figure out why Code Contracts thinks comparer can't be null.
			return source.OrderBy(_ => _, comparer ?? Comparer<T>.Default);
		}

		/// <summary>
		///     Sorts the elements of a sequence in descending order according to themselves.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <returns>An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted according to themselves.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
		[Pure]
		public static IOrderedQueryable<T> OrderDescending<T>(this IQueryable<T> source)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedQueryable<T>>() != null);

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
		public static IOrderedQueryable<T> OrderDescending<T>(this IQueryable<T> source, IComparer<T> comparer)
		{
			Contract.Requires<ArgumentNullException>(source != null);
			Contract.Ensures(Contract.Result<IOrderedQueryable<T>>() != null);

			// TODO: Figure out why Code Contracts thinks comparer can't be null.
			return source.OrderByDescending(_ => _, comparer ?? Comparer<T>.Default);
		}
	}
}