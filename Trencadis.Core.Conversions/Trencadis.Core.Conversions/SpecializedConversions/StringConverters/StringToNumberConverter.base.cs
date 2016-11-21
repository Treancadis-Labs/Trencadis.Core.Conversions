// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNumberConverter.base.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using Trencadis.Core.Conversions.Helpers;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Base class for conversions from string to numeric types
	/// </summary>
	/// <typeparam name="TNumber">The numeric type to convert to</typeparam>
	public abstract class StringToNumberConverter<TNumber> : SpecializedConverter<string, TNumber>
		where TNumber : struct
	{
		/// <summary>
		/// Gets the <see cref="NumberStyles"/> that are supported for numeric string parsing
		/// </summary>
		/// <returns>The <see cref="NumberStyles"/> that will be used for parsing</returns>
		protected virtual NumberStyles GetParseNumberStyles()
		{
			return NumberStyles.Number |
					NumberStyles.AllowDecimalPoint |
					NumberStyles.AllowThousands |
					NumberStyles.AllowLeadingSign |
					NumberStyles.AllowLeadingWhite |
					NumberStyles.AllowParentheses |
					NumberStyles.AllowTrailingSign |
					NumberStyles.AllowTrailingWhite;
		}

		/// <summary>
		/// Tries to parse the specified string and convert it to the numeric type
		/// </summary>
		/// <param name="input">The string input to be parsed</param>
		/// <param name="style">The supported <see cref="NumberStyles"/></param>
		/// <param name="format">The format used for parsing</param>
		/// <param name="result">The parsing result</param>
		/// <returns>True if parsing succeeds, false otherwise</returns>
		protected abstract bool TryParseNumber(string input, NumberStyles style, IFormatProvider format, out TNumber result);

		/// <inheritdoc />
		protected override TNumber ConvertValue(string value, TNumber defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			TNumber parseResult;
			if (this.TryParseNumber(value, this.GetParseNumberStyles(), format, out parseResult))
			{
				return parseResult;
			}

			if (conversionObserver != null)
			{
				conversionObserver.NotifyKnownFallbackToDefaultValue(
					value: value,
					targetType: typeof(TNumber),
					defaultTargetValue: defaultValue,
					format: format,
					fallbackReason: new FormatException(
										string.Format(
											"The passed string '{0}' format is not a parsable numeric (of type {1}) value",
											value,
											typeof(TNumber))));
			}

			return defaultValue;
		}
	}
}
