// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNumberConverter.to-long.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using System.Globalization;

	/// <summary>
	/// Specialized class for conversions from string to long
	/// </summary>
	public class StringToLongConverter : StringToNumberConverter<long>
	{
		/// <summary>
		/// Tries to parse the specified string and convert it to long
		/// </summary>
		/// <param name="input">The string input to be parsed</param>
		/// <param name="style">The supported <see cref="NumberStyles"/></param>
		/// <param name="format">The format used for parsing</param>
		/// <param name="result">The parsing result</param>
		/// <returns>True if parsing succeeds, false otherwise</returns>
		protected override bool TryParseNumber(string input, NumberStyles style, IFormatProvider format, out long result)
		{
			return long.TryParse(input, style, format, out result);
		}
	}
}
