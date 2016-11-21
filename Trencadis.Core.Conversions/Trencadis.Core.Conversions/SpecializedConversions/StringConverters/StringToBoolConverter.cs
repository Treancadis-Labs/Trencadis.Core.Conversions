// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToBoolConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Specialized class for conversions from string to boolean
	/// </summary>
	public class StringToBoolConverter : SpecializedConverter<string, bool>
	{
		/// <summary>
		/// Returns a flag indicating whether the value can be assimilated to a boolean true.
		/// By default the following strings are considered "true"-ish values: "true", "1", "y", "yes".
		/// Override if you want to consider other strings as true-ish values.
		/// </summary>
		/// <param name="value">The string to check</param>
		/// <returns>True if the value can be assimilated to a boolean true, false otherwise</returns>
		protected virtual bool IsTrueishValue(string value)
		{
			return string.Equals(value, true.ToString(), StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, 1.ToString(), StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, "y", StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Returns a flag indicating whether the value can be assimilated to a boolean false.
		/// By default the following strings are considered "false"-ish values: "false", "0", "n", "no".
		/// Override if you want to consider other strings as true-ish values.
		/// </summary>
		/// <param name="value">The string to check</param>
		/// <returns>True if the value can be assimilated to a boolean false, false otherwise</returns>
		protected virtual bool IsFalseishValue(string value)
		{
			return string.Equals(value, false.ToString(), StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, 0.ToString(), StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, "n", StringComparison.OrdinalIgnoreCase) ||
					string.Equals(value, "no", StringComparison.OrdinalIgnoreCase);
		}

		/// <inheritdoc />
		protected override bool ConvertValue(string value, bool defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			if (this.IsTrueishValue(value))
			{
				return true;
			}

			if (this.IsFalseishValue(value))
			{
				return false;
			}

			if(conversionObserver != null)
			{
				conversionObserver.NotifyKnownFallbackToDefaultValue(
					value: value,
					targetType: typeof(bool),
					defaultTargetValue: defaultValue,
					format: format,
					fallbackReason: new FormatException(
						string.Format(
							"The specified string '{0}' doesn't represent a true-ish or false-ish value, returning specified default value ('{1}') instead",
							value,
							defaultValue)));
			}

			return defaultValue;
		}
	}
}
