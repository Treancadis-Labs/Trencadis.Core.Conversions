// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversionsHelper.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using Trencadis.Core.Conversions.Exceptions;

	/// <summary>
	/// Conversions helper class
	/// </summary>
	internal static class ConversionsHelper
	{
		/// <summary>
		/// Gets the default value for the specified type
		/// </summary>
		/// <param name="type">The type for which we want the default value</param>
		/// <returns>The default value</returns>
		public static object GetDefaultValue(Type type)
		{
			// TODO: improve performance by
			// handling defaults for known types
			// return null for reference types

			if (type == null)
			{
				return null;
			}

			TypeInfo typeInfo = type.GetTypeInfo();
			if (typeInfo.IsValueType)
			{
				return Activator.CreateInstance(type);
			}

			return null;
		}

		public static bool HasImplicitConversionOperator(object value, Type toType, out object castResult)
		{
			castResult = null;

			if ((value == null) || (toType == null))
			{
				return false;
			}

			var fromType = value.GetType();

			var fromTypeInfo = fromType.GetTypeInfo();

			var implicitOperators = fromTypeInfo.GetDeclaredMethods("op_Implicit");
			if(implicitOperators != null)
			{
				var implop = implicitOperators.Where(op => op.ReturnType == toType).FirstOrDefault();
				if (implop != null)
				{
					castResult = implop.Invoke(null, new[] { value });
					return true;
				}
			}

			return false;
		}

		public static bool HasExplicitConversionOperator(object value, Type toType, out object castResult)
		{
			castResult = null;

			if ((value == null) || (toType == null))
			{
				return false;
			}

			var fromType = value.GetType();

			var fromTypeInfo = fromType.GetTypeInfo();

			var explicitOperators = fromTypeInfo.GetDeclaredMethods("op_Explicit");
			if (explicitOperators != null)
			{
				var explop = explicitOperators.Where(op => op.ReturnType == toType).FirstOrDefault();
				if (explop != null)
				{
					castResult = explop.Invoke(null, new[] { value });
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Checks whether the specified type is a generic type
		/// </summary>
		/// <param name="typeInfo">The checked type info</param>
		/// <returns>True if we're dealing with a generic type, false otherwise</returns>
		public static bool IsGenericType(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}

			return typeInfo.IsGenericType;
		}

		/// <summary>
		/// Checks whether the specified type is a nullable type (Nullable&lt;T&gt;) and if true, tries to extract the underlying generic argument type
		/// </summary>
		/// <param name="type">The type that is tested whether it's a nullable type or not</param>
		/// <param name="genericArgumentType">Output parameter: the underlying type for nullable types, null otherwise</param>
		/// <returns>True if the specified type is a nullable type (Nullable&lt;T&gt;), false otherwise</returns>
		public static bool TryParseNullableType(Type type, out Type genericArgumentType)
		{
			genericArgumentType = null;
			if (type == null)
			{
				return false;
			}

			Type genericType = type.GetGenericTypeDefinition();
			bool isNullable = genericType.Equals(typeof(Nullable<>));

			if (isNullable)
			{
				genericArgumentType = genericType.GenericTypeArguments[0];
			}

			return isNullable;
		}

		/// <summary>
		/// Checks whether the specified instance is a DBNull.Value instance.
		/// Since we're in a portable library we cannot use System.DBNull type, so we're relying on the type name instead
		/// </summary>
		/// <param name="instance">The instance to be checked</param>
		/// <returns>True if we're dealing with a DBNull value, false otherwise</returns>
		public static bool IsDBNull(object instance)
		{
			if (instance == null)
			{
				return false;
			}

			Type instanceType = instance.GetType();

			return string.Equals(instanceType.Name, "DBNull", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Chekcs whether the specified value represents a string-empty or string whitespaces-only value
		/// </summary>
		/// <param name="value">The value to be checked</param>
		/// <returns>True if  the specified value represents a string-empty or string whitespaces-only value, false otherwise</returns>
		public static bool IsStringEmptyOrWhitespace(object value)
		{
			if ((value != null) && (value.GetType() == typeof(System.String)))
			{
				var stringValue = value as string;

				return string.IsNullOrWhiteSpace(stringValue);
			}

			return false;
		}

		/// <summary>
		/// Formats the message for the TypeConversionException
		/// </summary>
		/// <param name="value">The value that the user tried to convert</param>
		/// <param name="defaultValue">The default value used</param>
		/// <param name="targetType">The target type</param>
		/// <param name="format">The format provider</param>
		/// <returns>A string containing the message for the generated TypeConversionException</returns>
		public static string FormatTypeConversionExceptionMesssage(object value, object defaultValue, Type targetType, IFormatProvider format)
		{
			string valueTypeName = (value != null) ? value.GetType().ToString() : "(undefined type)";
			string valueString = (value != null) ? (IsDBNull(value) ? "[NULL]" : value.ToString()) : "(null)";

			string defaultValueTypeName = (defaultValue != null) ? defaultValue.GetType().ToString() : "(undefined type)";
			string defaultValueString = (defaultValue != null) ? (IsDBNull(defaultValue) ? "[NULL]" : defaultValue.ToString()) : "(null)";

			string targetTypeName = (targetType != null) ? targetType.ToString() : "(undefined type)";

			string formatProviderTypeName = (format != null) ? format.GetType().ToString() : "(undefined type)";
			string cultureName = (format != null) ? format.ToString() : string.Empty;

			return string.Format(
				"Error(s) occured while trying to convert value '{0}' (of type '{1}') to type '{2}' using default value '{3}' (of type '{4}') and format provider '{5}' (of type '{6}')",
				valueString,
				valueTypeName,
				targetTypeName,
				defaultValueString,
				defaultValueString,
				cultureName,
				formatProviderTypeName);
		}
	}
}
