using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="IDictionary{TKey,TValue}" /> interface.
	/// </summary>
	public static class ForIDictionary
	{
		/// <summary>
		///     Increments the element with the provided key by one. If no element exists, it is set to one instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="key">The object to use as the key of the element to increment or add.</param>
		/// <returns>The value of the element after incrementing.</returns>
		public static int Increment<TKey>(this IDictionary<TKey, int> dictionary, TKey key)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);

			int value;
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary[key] = value + 1;
				return value + 1;
			}
			dictionary.Add(key, 1);
			return 1;
		}

		/// <summary>
		///     Increments the element with the provided key by the provided step. If no element exists, it is set to the step
		///     value instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="key">The object to use as the key of the element to increment or add.</param>
		/// <param name="step">The number to increment the element by.</param>
		/// <returns>The value of the element after incrementing.</returns>
		public static int IncrementBy<TKey>(this IDictionary<TKey, int> dictionary, TKey key, int step)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);

			int value;
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary[key] = value + step;
				return value + step;
			}
			dictionary.Add(key, step);
			return step;
		}
	}
}