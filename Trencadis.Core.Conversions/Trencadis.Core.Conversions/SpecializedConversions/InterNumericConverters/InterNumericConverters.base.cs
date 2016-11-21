// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="InterNumericConverters.base.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.InterNumericConverters
{
	using System;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Base class for inter-numeric conversions.
	/// Used for conversions where usage of explicit cast operators would likely result in numeric overflow (or possibly other) errors
	/// </summary>
	/// <typeparam name="TNumberFrom">The numeric type from which we are converting</typeparam>
	/// <typeparam name="TNumberTo">The numeric type to which we are converting</typeparam>
	public abstract class InterNumericConverters<TNumberFrom, TNumberTo> : SpecializedConverter<TNumberFrom, TNumberTo>
		where TNumberFrom : struct, IComparable<TNumberFrom>
		where TNumberTo : struct, IComparable<TNumberTo>
	{
		/// <summary>
		/// Gets a flag indicating whether casting current value to the specified numeric type will result in an overflow
		/// </summary>
		/// <param name="value">The decimal value</param>
		/// <param name="minValue">Output parameter: the numeric min value</param>
		/// <param name="maxValue">Output parameter: the numeric max value</param>
		/// <returns>True if overflow will occur, false otherwise</returns>
		protected abstract bool IsNumericOverflow(TNumberFrom value, out TNumberTo minValue, out TNumberTo maxValue);

		/// <summary>
		/// Tries to cast the specified decimal to the numeric type and returns a flag indicating whether casting could be performed.
		/// The purpose of this method is to gracefully avoid overflow exceptions when casting from decimal to other numeric types
		/// </summary>
		/// <param name="value">The decimal value</param>
		/// <param name="result">The cast result if the decimal value can be parsed, default numeric value otherwise</param>
		/// <returns>True if casting succeeds, false otherwise</returns>
		protected abstract bool TryCastToNumber(TNumberFrom value, out TNumberTo result);

		/// <inheritdoc />
		protected override TNumberTo ConvertValue(TNumberFrom value, TNumberTo defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			TNumberTo minValue, maxValue;
			if (this.IsNumericOverflow(value, out minValue, out maxValue))
			{
				if (conversionObserver != null)
				{
					conversionObserver.NotifyKnownFallbackToDefaultValue(
						value: value,
						targetType: typeof(TNumberTo),
						defaultTargetValue: defaultValue,
						format: format,
						fallbackReason: new OverflowException(
											string.Format(
												"The passed value (type {0}, value: {1}) exceeds the target numeric type range (target type: {2}, min-value: {3}, max-value: {4})",
												typeof(TNumberFrom),
												value,
												typeof(TNumberTo),
												minValue,
												maxValue)));
				}

				return defaultValue;
			}

			TNumberTo castedValue;
			if (this.TryCastToNumber(value, out castedValue))
			{
				return castedValue;
			}
			else
			{
				if (conversionObserver != null)
				{
					conversionObserver.NotifyKnownFallbackToDefaultValue(
						value: value,
						targetType: typeof(TNumberTo),
						defaultTargetValue: defaultValue,
						format: format,
						fallbackReason: new InvalidCastException(
											string.Format(
												"The passed value (type {0}, value: {1}) cannot be casted to the specified target numeric type({2})",
												typeof(TNumberFrom),
												value,
												typeof(TNumberTo))));
				}

				return defaultValue;
			}
		}

	}
}
