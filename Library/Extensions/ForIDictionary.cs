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
		///     Gets the element associated with the specified key, or adds an element with the provided key and value if none
		///     exists.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to get or add.</param>
		/// <param name="key">The object to use as the key of the element.</param>
		/// <param name="newValue">The object to use as the value of the element to add if none exists.</param>
		/// <returns>The existing element, or the element added.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="key" /> is <c>null</c>.</exception>
		public static TValue GetOrAdd<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary, TKey key, TValue newValue)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);

			TValue result;
			if (dictionary.TryGetValue(key, out result) == false)
			{
				result = newValue;
				dictionary.Add(key, result);
			}
			return result;
		}

		/// <summary>
		///     Gets the element associated with the specified key, or adds an element with the provided key and created value if
		///     none exists.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to get or add.</param>
		/// <param name="key">The object to use as the key of the element.</param>
		/// <param name="newValueFactory">A factory to create the object to use as the value of the element to add if none exists.</param>
		/// <returns>The existing element, or the element created and added.</returns>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="dictionary" />, <paramref name="key" />, or <paramref name="newValueFactory" /> is <c>null</c>.
		/// </exception>
		public static TValue GetOrAdd<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValueFactory)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);
			Contract.Requires<ArgumentNullException>(newValueFactory != null);

			TValue result;
			if (dictionary.TryGetValue(key, out result) == false)
			{
				result = newValueFactory();
				dictionary.Add(key, result);
			}
			return result;
		}

		/// <summary>
		///     Increments the element with the provided key by one. If no element exists, it is set to one instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="key">The object to use as the key of the element to increment or add.</param>
		/// <returns>The value of the element after incrementing.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="key" /> is <c>null</c>.</exception>
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
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="key" /> is <c>null</c>.</exception>
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

		/// <summary>
		///     Updates the element with the provided key and transform, or adds an element with the provided key and value if none
		///     exists.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to transform.</param>
		/// <param name="key">The object to use as the key of the element to transform or add.</param>
		/// <param name="valueTransform">The transform to apply to an existing element.</param>
		/// <param name="newValue">The object to use as the value of the element to add if none exists.</param>
		/// <returns>The transformed element, or the element added.</returns>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="dictionary" />, <paramref name="key" />, or <paramref name="valueTransform" /> is <c>null</c>.
		/// </exception>
		public static TValue UpdateOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
			TKey key, Func<TValue, TValue> valueTransform, TValue newValue)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);
			Contract.Requires<ArgumentNullException>(valueTransform != null);

			TValue result;
			if (dictionary.TryGetValue(key, out result) == false)
			{
				result = newValue;
				dictionary.Add(key, result);
			}
			else
			{
				result = valueTransform(result);
				dictionary[key] = result;
			}
			return result;
		}

		/// <summary>
		///     Updates the element with the provided key and transform, or adds an element with the provided key and created value
		///     if none exists.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to transform.</param>
		/// <param name="key">The object to use as the key of the element to transform or add.</param>
		/// <param name="valueTransform">The transform to apply to an existing element.</param>
		/// <param name="newValueFactory">A factory to create the object to use as the value of the element to add if none exists.</param>
		/// <returns>The transformed element, or the element created and added.</returns>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="dictionary" />, <paramref name="key" />, <paramref name="valueTransform" />, or
		///     <paramref name="newValueFactory" /> is <c>null</c>.
		/// </exception>
		public static TValue UpdateOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
			TKey key, Func<TValue, TValue> valueTransform, Func<TValue> newValueFactory)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);
			Contract.Requires<ArgumentNullException>(valueTransform != null);
			Contract.Requires<ArgumentNullException>(newValueFactory != null);

			TValue result;
			if (dictionary.TryGetValue(key, out result) == false)
			{
				result = newValueFactory();
				dictionary.Add(key, result);
			}
			else
			{
				result = valueTransform(result);
				dictionary[key] = result;
			}
			return result;
		}
	}
}