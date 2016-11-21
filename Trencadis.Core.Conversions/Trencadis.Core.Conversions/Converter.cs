// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Converter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions
{
	using System;
	using System.Reflection;
	using Trencadis.Core.Conversions.Exceptions;
	using Trencadis.Core.Conversions.Helpers;
	using Trencadis.Core.Conversions.Infrastructure;
	using Trencadis.Core.Conversions.Infrastructure.Bootstrapping;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;
	using Trencadis.Core.Conversions.SpecializedConversions;

	/// <summary>
	/// Converter class
	/// </summary>
	public static class Converter
	{
		/// <summary>
		/// Conversions settings
		/// </summary>
		public static class Settings
		{
			/// <summary>
			/// Gets the specialized conversions bootstrapper
			/// </summary>
			public static ISpecializedConversionsBootstrapper SpecializedConversionsBootstrapper
			{
				get;
				set;
			}
		}

		/// <summary>
		/// Converts a value to the specified type
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="targetType">The type to which the value should be converted</param>
		/// <param name="targetDefaultValue">The default value to use instead of null or DBNull.Value</param>
		/// <param name="format">The format used for conversion</param>
		/// <param name="conversionObserver">Conversion observer</param>
		/// <returns>The converted value</returns>
		public static object Convert(object value, Type targetType, object targetDefaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}

			TypeInfo targetTypeInfo = targetType.GetTypeInfo();
			if (targetTypeInfo == null)
			{
				throw new ArgumentException(string.Format("Unable to get type information for type {0}", targetType));
			}

			if (targetTypeInfo.IsValueType && ((targetDefaultValue == null) || (ConversionsHelper.IsDBNull(targetDefaultValue))))
			{
				// converting to a value type, but having the default value as null / DBNull
				targetDefaultValue = ConversionsHelper.GetDefaultValue(targetType);
			}

			if ((value == null) || ConversionsHelper.IsDBNull(value) || ConversionsHelper.IsStringEmptyOrWhitespace(value))
			{
				// converting null / DBNull / empty or whitespaces-only string to target type => just return default value
				if(conversionObserver != null)
				{
					conversionObserver.NotifyKnownFallbackToDefaultValue(
						value: value,
						targetType: targetType,
						defaultTargetValue: targetDefaultValue,
						format: format,
						fallbackReason: new ArgumentNullException("value"));
				}

				return targetDefaultValue;
			}

			object converted = targetDefaultValue;

			try
			{
				// Handle Nullable<T>: try to convert to underlying T
				bool targetIsGenericType = ConversionsHelper.IsGenericType(targetTypeInfo);
				if (targetIsGenericType)
				{
					Type nullableTypeArg;
					if (ConversionsHelper.TryParseNullableType(targetType, out nullableTypeArg))
					{
						targetType = nullableTypeArg;
					}
				}

				var bootstrapper = Converter.Settings.SpecializedConversionsBootstrapper;
				if (bootstrapper == null)
				{
					bootstrapper = new DefaultSpecializedConversionsBootstrapper();
				}

				var converter = SpecializedConverterLocator.GetSpecializedConverter(value.GetType(), targetType, bootstrapper.DiscoveredSpecializedConverters);
				if (converter == null)
				{
					converter = new GenericConverter(targetType);
				}

				converted = converter.Convert(value, targetDefaultValue, format, conversionObserver);
			}
			catch (Exception ex)
			{
				if (
					// Convert.ChangeType may throw InvalidCastException when:
					//   - This conversion is not supported OR
					//   - value is null and conversionType is a value type OR
					//   - value does not implement the IConvertible interface
					(ex is InvalidCastException) ||

					// Convert.ChangeType may throw FormatException when :
					//   - value is not in a format for conversionType recognized by provider.
					(ex is FormatException) ||

					// Convert.ChangeType may throw OverflowException when:
					//   - value represents a number that is out of the range of conversionType.
					// - or -
					// Enum.Parse may throw OverflowException when:
					//   - value is outside the range of the underlying type of enumType
					(ex is OverflowException) ||

					// Convert.ChangeType may throw ArgumentNullException when :
					//   - conversionType is null.
					// - or -
					// Enum.IsDefined may throw System.ArgumentNullException when:
					//   - enumType or value is null
					// - or -
					// Enum.Parse may throw ArgumentNullException when :
					//   - enumType or value is null.
					(ex is ArgumentNullException) ||

					// Enum.IsDefined may throw System.ArgumentException when:
					//   - enumType is not an Enum.
					//   - The type of value is an enumeration, but it is not an enumeration of type enumType.
					//   - The type of value is not an underlying type of enumType.
					// - or -
					// Enum.Parse may throw ArgumentException when:
					//   - enumType is not an Enum
					//   - value is either an empty string or only contains white space
					//   - value is a name, but not one of the named constants defined for the enumeration
					(ex is ArgumentException) ||

					// Enum.IsDefined may throwSystem.InvalidOperationException when:
					//   - value is not type System.SByte, System.Int16, System.Int32, System.Int64, System.Byte, System.UInt16, System.UInt32, or System.UInt64, or System.String.
					(ex is InvalidOperationException))
				{
					if (conversionObserver != null)
					{
						conversionObserver.NotifyCaughtConversionException(
							value: value,
							targetType: targetType,
							defaultTargetValue: targetDefaultValue,
							format: format,
							conversionException: ex);
					}
				}
				else
				{
					throw;
				}
			}

			return converted;
		}
	}
}
