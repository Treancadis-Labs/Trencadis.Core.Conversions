// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectConversionExtensions.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions
{
	using System;
	using Trencadis.Core.Conversions.Exceptions;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Object conversion extensions
	/// </summary>
	public static class ConversionExtensions
	{
		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: default(TTarget),
				format: System.Globalization.CultureInfo.CurrentCulture,
				conversionObserver: null);
		}

		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <param name="conversionObserver">Conversion observer</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value, IConversionObserver conversionObserver)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: default(TTarget),
				format: System.Globalization.CultureInfo.CurrentCulture,
				conversionObserver: conversionObserver);
		}

		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <param name="format">The format to use for conversion</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value, IFormatProvider format)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: default(TTarget),
				format: format,
				conversionObserver: null);
		}

		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <param name="defaultValue">Target default value to use for null or conversion error(s)</param>
		/// <param name="conversionObserver">Conversion observer</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value, TTarget defaultValue, IConversionObserver conversionObserver)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: defaultValue,
				format: System.Globalization.CultureInfo.CurrentCulture,
				conversionObserver: conversionObserver);
		}

		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <param name="defaultValue">Target default value to use for null or conversion error(s)</param>
		/// <param name="format">The format to use for conversion</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value, TTarget defaultValue, IFormatProvider format)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: defaultValue,
				format: format,
				conversionObserver: null);
		}

		/// <summary>
		/// Converts the value to the specified target type
		/// </summary>
		/// <typeparam name="TTarget">Target type</typeparam>
		/// <param name="value">The value to be converted</param>
		/// <param name="defaultValue">Target default value to use for null or conversion error(s)</param>
		/// <param name="format">The format to use for conversion</param>
		/// <param name="conversionObserver">Conversion observer</param>
		/// <returns>The converted value</returns>
		public static TTarget ConvertTo<TTarget>(this object value, TTarget defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			return (TTarget)Converter.Convert(
				value: value,
				targetType: typeof(TTarget),
				targetDefaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);
		}
	}
}
