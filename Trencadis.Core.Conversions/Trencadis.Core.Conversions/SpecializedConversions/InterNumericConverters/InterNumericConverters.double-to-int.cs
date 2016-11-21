// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="InterNumericConverters.double-to-int.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.InterNumericConverters
{
	/// <summary>
	/// Specialized class for conversions from double to int
	/// </summary>
	public class DoubleToIntConverter : InterNumericConverters<double, int>
	{
		/// <inheritdoc />
		protected override bool IsNumericOverflow(double value, out int minValue, out int maxValue)
		{
			minValue = int.MinValue;
			maxValue = int.MaxValue;

			return (value < (double)minValue) || (value > (double)maxValue);
		}

		/// <inheritdoc />
		protected override bool TryCastToNumber(double value, out int result)
		{
			if (double.IsNaN(value))
			{
				result = default(int);
				return false;
			}

			result = (int)value;
			return true;
		}
	}
}
