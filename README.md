BitFn.Core [![Build status](https://ci.appveyor.com/api/projects/status/oy6i3wdm7mjht6i7/branch/master?svg=true)](https://ci.appveyor.com/project/dlras2/core/branch/master)
======
A general utility library.

#### Release Notes:
- **Upcoming**
  - Added `string.Unescape` and `string.UnescapeVerbatim`
- **0.1.8**
  - Added `IEnumerable<IEnumerable<T>>.Aggregate`
  - Added `IDictionary<TKey, ISet<TValue>>.AddTo` and `IDictionary<TKey, IList<TValue>>.AddTo`
  - Changed target framework from 4.5.2 to 4.5.1
- **0.1.7**
  - Added XML documentation output
  - Added Symbol publishing
- **0.1.6**
  - Added `IDictionary<T, int>.IncrementAll` and step and kvp overloads
  - Renamed `IDictionary.IncrementBy` to be an overload of `IDictionary.Increment`
- **0.1.5**
  - Added `IEnumerable<T>.CountBy`, `IEnumerable<T>.CountByMany`, and comparer overloads
  - Added `IEnumerable<T>.ThrowIfAny` and exception overloads
  - Added `IDictionary<T, int>.Increment` and `IDictionary<T, int>.IncrementBy`
  - Added `IDictionary<TKey, TValue>.GetOrAdd` and factory overload
  - Added `IDictionary<TKey, TValue>.UpdateOrAdd` and factory overload
- **0.1.4**
  - Added Code Contracts throughout
  - Changed and standardized XML documentation
- **0.1.3**
  - Added `RandomInt`, `RandomDouble`, etc. delegates
  - Added `ThreadSafeRandom.RandomInt`, `ThreadSafeRandom.RandomDouble`, etc. properties
  - Added `IEnumerable<KeyValuePair<TKey, TValue>>.ToDictionary` and comparer overload
  - Added `IEnumerable<T>.Order`, `IEnumerable<T>.OrderDescending`, and comparer overloads
  - Added `IQueryable<T>.Order`, `IQueryable<T>.OrderDescending`, and comparer overloads
  - Changed `IEnumerable<T>.Shuffle` rng overload to consume rng delegate
- **0.1.2**
  - Added `string.RemoveDiacritics`
  - Added `string.ToSlug`
- **0.1.1**
  - Added `IEnumerable<T>.Shuffle` and rng overload
  - Added `ThreadSafeRandom.Local`
- **0.1.0** Initial release.
  - Added `Char.ToHex`
  - Added `Expression<Func<TSource, TProperty>>.GetPropertyInfo`
  - Added `StringBuilder.Append` overload with `repeatCount`
