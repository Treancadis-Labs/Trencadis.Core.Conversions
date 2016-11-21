// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToCharConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Specialized class for conversions from string to char
	/// </summary>
	public class StringToCharConverter : SpecializedConverter<string, char>
	{
		/// <summary>
		/// Gets the char array from the specified string.
		/// Override if you need additional manipulation of the char array, except what string.ToCharArray() already does
		/// </summary>
		/// <param name="value">The string to be split in characters</param>
		/// <returns>The char array that compose the string value</returns>
		protected virtual char[] GetCharArray(string value)
		{
			return value.ToCharArray();
		}

		/// <inheritdoc />
		protected override char ConvertValue(string value, char defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			var charArray = this.GetCharArray(value);

			if ((charArray != null) && (charArray.Length > 0))
			{
				return charArray[0];
			}

			if (conversionObserver != null)
			{
				conversionObserver.NotifyKnownFallbackToDefaultValue(
					value: value,
					targetType: typeof(char),
					defaultTargetValue: defaultValue,
					format: format,
					fallbackReason: new ArgumentException(
						string.Format(
							"The specified string '{0}' can't be split into a non-zero length char array, using specified default value ('{1}') instead",
							value,
							defaultValue)));
			}

			return defaultValue;
		}
	}
}
