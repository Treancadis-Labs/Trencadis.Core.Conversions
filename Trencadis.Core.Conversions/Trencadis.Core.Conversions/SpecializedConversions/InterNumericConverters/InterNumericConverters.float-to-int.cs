// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="InterNumericConverters.float-to-int.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.InterNumericConverters
{
	/// <summary>
	/// Specialized class for conversions from float to int
	/// </summary>
	public class FloatToIntConverter : InterNumericConverters<float, int>
	{
		/// <inheritdoc />
		protected override bool IsNumericOverflow(float value, out int minValue, out int maxValue)
		{
			minValue = int.MinValue;
			maxValue = int.MaxValue;

			return (value < (float)minValue) || (value > (float)maxValue);
		}

		/// <inheritdoc />
		protected override bool TryCastToNumber(float value, out int result)
		{
			if (float.IsNaN(value))
			{
				result = default(int);
				return false;
			}

			result = (int)value;
			return true;
		}
	}
}
