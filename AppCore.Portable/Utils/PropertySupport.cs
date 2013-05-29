using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mt.Common.AppCore.Utils
{
	///<summary>
	/// Provides support for extracting property information based on a property expression.
	/// This code is c/p from Prism4
	///</summary>
	public static class PropertySupport
	{
		/// <summary>
		/// Extracts the property name from a property expression.
		/// </summary>
		/// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
		/// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
		/// <returns>The name of the property.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown when the expression is:<br/>
		///     Not a <see cref="MemberExpression"/><br/>
		///     The <see cref="MemberExpression"/> does not represent a property.<br/>
		///     Or, the property is static.
		/// </exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			return ExtractPropertyName<T>(propertyExpression, false);
		}

		/// <summary>
		/// Extracts the property name from a property expression.
		/// </summary>
		/// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
		/// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName)</param>
		/// <param name="checkStatic">If true and expression has static property exception will throw</param>
		/// <returns>The name of the property.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="propertyExpression"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown when the expression is:<br/>
		///     Not a <see cref="MemberExpression"/><br/>
		///     The <see cref="MemberExpression"/> does not represent a property.<br/>
		///     Or, the property is static.
		/// </exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression, bool checkStatic)
		{
			if(propertyExpression == null)
				throw new ArgumentNullException("propertyExpression");

			MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
			if(memberExpression == null)
				throw new ArgumentException("The expression is not a member access expression", "propertyExpression");

			PropertyInfo property = memberExpression.Member as PropertyInfo;
			if(property == null)
				throw new ArgumentException("The member access expression does not access a property", "propertyExpression");

			MethodInfo getMethod = property.GetMethod;
			if(checkStatic && getMethod.IsStatic)
				throw new ArgumentException("The referenced property is a static property", "propertyExpression");

			return memberExpression.Member.Name;
		}
	}
}
