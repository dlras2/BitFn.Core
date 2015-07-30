using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace BitFn.Core.Extensions
{
	/// <summary>
	///     Extension methods for the <see cref="Expression" /> class.
	/// </summary>
	public static class ForExpression
	{
		/// <summary>
		///     Gets the <see cref="PropertyInfo" /> for the property represented by the given expression.
		/// </summary>
		/// <typeparam name="TSource">The type implementing the property.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="property">An expression of the property whose info to get.</param>
		/// <returns>The <see cref="PropertyInfo" /> for the property represented by the given expression.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException"><paramref name="property" /> does not refer to a property from the given type.</exception>
		/// <remarks>
		///     Adapted from Cameron MacFarland's answer on StackOverflow.
		///     http://stackoverflow.com/users/3820/
		///     http://stackoverflow.com/a/672212/343238
		/// </remarks> 
		[Pure]
		public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
			this Expression<Func<TSource, TProperty>> property)
		{
			Contract.Requires<ArgumentNullException>(property != null);
			Contract.Ensures(Contract.Result<PropertyInfo>() != null);

			var member = property.Body as MemberExpression;
			if (member == null)
			{
				throw new ArgumentException($"Expression '{property}' refers to a method, not a property.");
			}
			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
			{
				throw new ArgumentException($"Expression '{property}' refers to a field, not a property.");
			}

			Contract.Assert(propInfo.ReflectedType != null);
			if (typeof (TSource) != propInfo.ReflectedType && !typeof (TSource).IsSubclassOf(propInfo.ReflectedType))
			{
				throw new ArgumentException(
					$"Expresion '{property}' refers to a property that is not from type {typeof (TSource)}.");
			}
			return propInfo;
		}
	}
}