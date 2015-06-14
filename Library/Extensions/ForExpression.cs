using System;
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
		///     Gets the property info for the property represented by the given lambda.
		/// </summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <typeparam name="TProperty">The property type.</typeparam>
		/// <param name="propertyLambda">The lambda representation of the property to get.</param>
		/// <returns>The property info for the property represented by the given lambda.</returns>
		/// <remarks>
		///     Adapted from Cameron MacFarland's answer on StackOverflow.
		///     http://stackoverflow.com/users/3820/
		///     http://stackoverflow.com/a/672212/343238
		/// </remarks>
		public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
			this Expression<Func<TSource, TProperty>> propertyLambda)
		{
			if (propertyLambda == null) throw new ArgumentNullException(nameof(propertyLambda));

			var member = propertyLambda.Body as MemberExpression;
			if (member == null)
			{
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
			}
			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
			{
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
			}
			if (propInfo.ReflectedType == null ||
			    (typeof (TSource) != propInfo.ReflectedType && !typeof (TSource).IsSubclassOf(propInfo.ReflectedType)))
			{
				throw new ArgumentException(
					$"Expresion '{propertyLambda}' refers to a property that is not from type {typeof (TSource)}.");
			}
			return propInfo;
		}
	}
}