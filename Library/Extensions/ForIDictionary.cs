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
		///     Adds an element to the list with the provided, or adds a new list with the provided key and the single element.
		/// </summary>
		/// <param name="dictionary">The dictionary whose list to add or add to.</param>
		/// <param name="key">The object to use as the key of the list.</param>
		/// <param name="value">The value to add to the list with the given key.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="key" /> is <c>null</c>.</exception>
		public static void AddTo<TKey, TValue>(
			this IDictionary<TKey, IList<TValue>> dictionary, TKey key, TValue value)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);

			IList<TValue> list;
			if (dictionary.TryGetValue(key, out list))
			{
				list.Add(value);
				return;
			}
			list = new List<TValue> {value};
			dictionary.Add(key, list);
		}

		/// <summary>
		///     Adds an element to the set with the provided, or adds a new set with the provided key and the single element.
		/// </summary>
		/// <param name="dictionary">The dictionary whose set to add or add to.</param>
		/// <param name="key">The object to use as the key of the set.</param>
		/// <param name="value">The value to add to the set with the given key.</param>
		/// <returns><c>true</c> if the element is added to the set; <c>false</c> if the element is already in the set.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="key" /> is <c>null</c>.</exception>
		public static bool AddTo<TKey, TValue>(
			this IDictionary<TKey, ISet<TValue>> dictionary, TKey key, TValue value)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(key != null);

			ISet<TValue> set;
			if (dictionary.TryGetValue(key, out set))
			{
				return set.Add(value);
			}
			set = new HashSet<TValue> {value};
			dictionary.Add(key, set);
			return true;
		}

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
		public static int Increment<TKey>(this IDictionary<TKey, int> dictionary, TKey key, int step)
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
		///     Increments each element with the provided keys by one. If no element for a given key exists, it is set to one
		///     instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="keys">The objects to use as the keys of the elements to increment or add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="keys" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="keys" /> contains <c>null</c>.</exception>
		public static void IncrementAll<TKey>(this IDictionary<TKey, int> dictionary, IEnumerable<TKey> keys)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(keys != null);

			foreach (var key in keys)
			{
				if (ReferenceEquals(key, null))
				{
					throw new ArgumentException("Null keys not allowed.", nameof(keys));
				}
				int value;
				if (dictionary.TryGetValue(key, out value))
				{
					dictionary[key] = value + 1;
				}
				else
				{
					dictionary.Add(key, 1);
				}
			}
		}

		/// <summary>
		///     Increments each element with the provided keys by the provided step. If no element for a given key exists, it is
		///     set to the step value instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="keys">The objects to use as the keys of the elements to increment or add.</param>
		/// <param name="step">The number to increment each element by.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary" /> or <paramref name="keys" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="keys" /> contains <c>null</c>.</exception>
		public static void IncrementAll<TKey>(this IDictionary<TKey, int> dictionary, IEnumerable<TKey> keys, int step)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(keys != null);

			foreach (var key in keys)
			{
				if (ReferenceEquals(key, null))
				{
					throw new ArgumentException("Null keys not allowed.", nameof(keys));
				}
				int value;
				if (dictionary.TryGetValue(key, out value))
				{
					dictionary[key] = value + step;
				}
				else
				{
					dictionary.Add(key, step);
				}
			}
		}

		/// <summary>
		///     Increments each element with the provided keys by the paired step. If no element for a given key exists, it is set
		///     to the paired step value instead.
		/// </summary>
		/// <param name="dictionary">The dictionary whose element to increment.</param>
		/// <param name="keyStepPairs">
		///     The objects to use as the key of the elements to increment or add, and their paired step
		///     value to increment by.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///     <paramref name="dictionary" /> or <paramref name="keyStepPairs" /> is
		///     <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException"><paramref name="keyStepPairs" /> contains a <c>null</c> key.</exception>
		public static void IncrementAll<TKey>(this IDictionary<TKey, int> dictionary,
			IEnumerable<KeyValuePair<TKey, int>> keyStepPairs)
		{
			Contract.Requires<ArgumentNullException>(dictionary != null);
			Contract.Requires<ArgumentNullException>(keyStepPairs != null);

			foreach (var kvp in keyStepPairs)
			{
				if (ReferenceEquals(kvp.Key, null))
				{
					throw new ArgumentException("Null keys not allowed.", nameof(keyStepPairs));
				}
				int value;
				if (dictionary.TryGetValue(kvp.Key, out value))
				{
					dictionary[kvp.Key] = value + kvp.Value;
				}
				else
				{
					dictionary.Add(kvp.Key, kvp.Value);
				}
			}
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