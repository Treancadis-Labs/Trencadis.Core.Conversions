// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecializedConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions
{
	using System;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Abstracts a class that is capable of performing a specialized type conversion
	/// </summary>
	public interface ISpecializedConverter
	{
		/// <summary>
		/// The type from which we are converting
		/// </summary>
		Type FromType { get; }

		/// <summary>
		/// The type to which we are converting
		/// </summary>
		Type ToType { get; }

		/// <summary>
		/// Converts the specified value, using the specified default value and format
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="defaultValue">The default value (used for null, or DBNull.Value)</param>
		/// <param name="format">The format</param>
		/// <param name="conversionObserver">The conversion observer</param>
		/// <returns>The converted value</returns>
		object Convert(object value, object defaultValue, IFormatProvider format, IConversionObserver conversionObserver);
	}
}
