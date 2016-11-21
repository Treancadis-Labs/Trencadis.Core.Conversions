// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="InterNumericConverters.decimal-to-int.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.InterNumericConverters
{
	/// <summary>
	/// Specialized class for conversions from decimal to int
	/// </summary>
	public class DecimalToIntConverter : InterNumericConverters<decimal, int>
	{
		/// <inheritdoc />
		protected override bool IsNumericOverflow(decimal value, out int minValue, out int maxValue)
		{
			minValue = int.MinValue;
			maxValue = int.MaxValue;

			return (value < (decimal)minValue) || (value > (decimal)maxValue);
		}

		/// <inheritdoc />
		protected override bool TryCastToNumber(decimal value, out int result)
		{
			result = (int)value;

			return true;
		}
	}
}
