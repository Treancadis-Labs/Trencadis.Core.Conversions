// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToGuidConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions.StringConverters
{
	using System;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Specialized class for conversions from string to Guid
	/// </summary>
	public class StringToGuidConverter : SpecializedConverter<string, Guid>
	{
		/// <inheritdoc />
		protected override Guid ConvertValue(string value, Guid defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			Guid parseResult;
			if(Guid.TryParse(value, out parseResult))
			{
				return parseResult;
			}

			if (conversionObserver != null)
			{
				conversionObserver.NotifyKnownFallbackToDefaultValue(
					value: value,
					targetType: typeof(Guid),
					defaultTargetValue: defaultValue,
					format: format,
					fallbackReason: new FormatException(
						string.Format(
							"The passed string '{0}' format is not a parsable Guid value",
							value)));
			}

			return defaultValue;
		}
	}
}
