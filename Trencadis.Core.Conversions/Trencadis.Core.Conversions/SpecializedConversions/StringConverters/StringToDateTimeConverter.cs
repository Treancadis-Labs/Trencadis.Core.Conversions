// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToDateTimeConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using System.Globalization;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Specialized class for conversions from string to DateTime
	/// </summary>
	public class StringToDateTimeConverter : SpecializedConverter<string, DateTime>
	{
		/// <summary>
		/// Gets the <see cref="NumberStyles"/> that are supported for numeric string parsing
		/// </summary>
		/// <returns>The <see cref="NumberStyles"/> that will be used for parsing</returns>
		protected virtual DateTimeStyles GetParseDateTimeStyles()
		{
			return DateTimeStyles.AllowInnerWhite |
					DateTimeStyles.AllowLeadingWhite |
					DateTimeStyles.AllowTrailingWhite |
					DateTimeStyles.AllowWhiteSpaces |
					DateTimeStyles.AssumeLocal;
		}

		/// <summary>
		/// Tries to parse the specified string and convert it to DateTime
		/// </summary>
		/// <param name="input">The string input to be parsed</param>
		/// <param name="style">The supported <see cref="DateTimeStyles"/></param>
		/// <param name="format">The format used for parsing</param>
		/// <param name="result">The parsing result</param>
		/// <returns>True if parsing succeeds, false otherwise</returns>
		protected virtual bool TryParseDateTime(string input, DateTimeStyles style, IFormatProvider format, out DateTime result)
		{
			return DateTime.TryParse(input, format, style, out result);
		}

		/// <inheritdoc />
		protected override DateTime ConvertValue(string value, DateTime defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			DateTime parsedDateTime;
			if (this.TryParseDateTime(value, this.GetParseDateTimeStyles(), format, out parsedDateTime))
			{
				return parsedDateTime;
			}

			if (conversionObserver != null)
			{
				conversionObserver.NotifyKnownFallbackToDefaultValue(
					value: value,
					targetType: typeof(DateTime),
					defaultTargetValue: defaultValue,
					format: format,
					fallbackReason: new FormatException(
						string.Format(
							"The passed string '{0}' format is not a parsable DateTime value",
							value)));
			}

			return defaultValue;
		}
	}
}
