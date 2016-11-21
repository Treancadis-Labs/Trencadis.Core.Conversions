// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSpecializedConversionsBootstrapper.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Infrastructure.Bootstrapping
{
	using Trencadis.Core.Conversions.SpecializedConversions.InterNumericConverters;
	using Trencadis.Core.Conversions.SpecializedConversions.StringConverters;

	/// <summary>
	/// Default specialized conversions bootstrapper
	/// </summary>
	public class DefaultSpecializedConversionsBootstrapper : SpecializedConversionsBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSpecializedConversionsBootstrapper"/>
		/// </summary>
		public DefaultSpecializedConversionsBootstrapper()
		{
		}

		/// <summary>
		/// Discoveres default specialized converters
		/// </summary>
		protected override void DiscoverConverters()
		{
			this.AddSpecializedConverter<StringToByteConverter>();
			this.AddSpecializedConverter<StringToSbyteConverter>();
			this.AddSpecializedConverter<StringToShortConverter>();
			this.AddSpecializedConverter<StringToUshortConverter>();
			this.AddSpecializedConverter<StringToIntConverter>();
			this.AddSpecializedConverter<StringToUintConverter>();
			this.AddSpecializedConverter<StringToLongConverter>();
			this.AddSpecializedConverter<StringToUlongConverter>();
			this.AddSpecializedConverter<StringToUlongConverter>();
			this.AddSpecializedConverter<StringToFloatConverter>();
			this.AddSpecializedConverter<StringToDoubleConverter>();
			this.AddSpecializedConverter<StringToDecimalConverter>();

			this.AddSpecializedConverter<DecimalToIntConverter>();
			this.AddSpecializedConverter<FloatToIntConverter>();
			this.AddSpecializedConverter<DoubleToIntConverter>();

			this.AddSpecializedConverter<StringToCharConverter>();
			this.AddSpecializedConverter<StringToBoolConverter>();
			this.AddSpecializedConverter<StringToDateTimeConverter>();
			this.AddSpecializedConverter<StringToGuidConverter>();
		}
	}
}
