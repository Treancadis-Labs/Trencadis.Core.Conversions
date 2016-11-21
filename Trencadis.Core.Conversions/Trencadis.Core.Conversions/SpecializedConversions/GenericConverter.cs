// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericConverter.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.SpecializedConversions
{
	using System;
	using System.Reflection;
	using Trencadis.Core.Conversions.Helpers;
	using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

	/// <summary>
	/// Generic type converter
	/// </summary>
	public class GenericConverter : SpecializedConverter<object, object>
	{
		/// <summary>
		/// Holds the type to which we are trying to convert
		/// </summary>
		private readonly Type toType;

		/// <summary>
		/// Holds the type-info of the type to which we are trying to convert
		/// </summary>
		private readonly TypeInfo toTypeInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="GenericConverter"/> class
		/// </summary>
		/// <param name="toType">The effective type to which we are trying to convert</param>
		/// <exception cref="ArgumentNullException">Thrown when toType is null</exception>
		public GenericConverter(Type toType)
		{
			if(toType == null)
			{
				throw new ArgumentNullException("toType");
			}

			this.toType = toType;

			this.toTypeInfo = this.toType.GetTypeInfo();
		}

		/// <summary>
		/// Gets the type to which we are trying to convert
		/// </summary>
		public override Type ToType
		{
			get
			{
				return this.toType;
			}
		}

		/// <summary>
		/// Converts the value to the target type using the provided default value and format
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="defaultValue">The default value to use for null or DBNull.Value</param>
		/// <param name="format">The format</param>
		/// <param name="conversionObserver">The conversion observer</param>
		/// <returns>The conversion result</returns>
		protected override object ConvertValue(object value, object defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			object converted = defaultValue;

			bool targetIsEnum = this.toTypeInfo.IsEnum;
			if (targetIsEnum && Enum.IsDefined(this.toType, value))
			{
				// handle Enums
				string stringEnumValue = value.ToString();
				if (!string.IsNullOrWhiteSpace(stringEnumValue))
				{
					converted = Enum.Parse(this.toType, stringEnumValue);
					return converted;
				}
			}
			else
			{
				// check for implicit operators
				object implicitOpResult;
				var hasImplicitConversion = ConversionsHelper.HasImplicitConversionOperator(value, this.ToType, out implicitOpResult);
				if(hasImplicitConversion)
				{
					converted = implicitOpResult;
					return converted;
				}

				// check for explicit operators
				object explicitOpResult;
				var hasExplicitConversion = ConversionsHelper.HasExplicitConversionOperator(value, this.ToType, out explicitOpResult);
				if (hasExplicitConversion)
				{
					converted = explicitOpResult;
					return converted;
				}

				converted = System.Convert.ChangeType(value, this.toType, format);
			}

			return converted;
		}
	}
}
