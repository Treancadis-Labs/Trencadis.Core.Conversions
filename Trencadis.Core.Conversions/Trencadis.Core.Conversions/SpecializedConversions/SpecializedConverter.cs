// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecializedConverter.cs" company="Trencadis">
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
	/// Base class for implementing specialized converters
	/// </summary>
	/// <typeparam name="TFrom">The type from which this converter is capable of converting</typeparam>
	/// <typeparam name="TTo">The type to which this converter is capable of converting</typeparam>
	public abstract class SpecializedConverter<TFrom, TTo> : ISpecializedConverter
	{
		/// <summary>
		/// Holds the type from which we are converting
		/// </summary>
		private readonly Type fromType;

		/// <summary>
		/// Holds the type-info of the type from which we are converting
		/// </summary>
		private readonly TypeInfo fromTypeInfo;

		/// <summary>
		/// Holds the type to which we are converting
		/// </summary>
		private readonly Type toType;

		/// <summary>
		/// Holds the type-info of the type to which we are converting
		/// </summary>
		private readonly TypeInfo toTypeInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="SpecializedConverter"/>
		/// </summary>
		public SpecializedConverter()
		{
			this.fromType = typeof(TFrom);

			this.fromTypeInfo = this.fromType.GetTypeInfo();

			this.toType = typeof(TTo);

			this.toTypeInfo = this.toType.GetTypeInfo();
		}

		/// <summary>
		/// Gets the type from which we are converting
		/// </summary>
		public virtual Type FromType
		{
			get
			{
				return this.fromType;
			}
		}

		/// <summary>
		/// Gets the type to which we are converting
		/// </summary>
		public virtual Type ToType
		{
			get
			{
				return this.toType;
			}
		}

		/// <summary>
		/// Effectively converts the specified value to the target type, using the provided default value and format
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="defaultValue">The default value (to use for null or DBNull.Value)</param>
		/// <param name="format">The format</param>
		/// <param name="conversionObserver">The conversion observer</param>
		/// <returns>The conversion result</returns>
		protected abstract TTo ConvertValue(TFrom value, TTo defaultValue, IFormatProvider format, IConversionObserver conversionObserver);

		/// <summary>
		/// Converts the specified value, using the specified default value and format
		/// </summary>
		/// <param name="value">The value to convert</param>
		/// <param name="defaultValue">The default value (used for null, or DBNull.Value)</param>
		/// <param name="format">The format</param>
		/// <param name="conversionObserver">The conversion observer</param>
		/// <returns>The converted value</returns>
		public object Convert(object value, object defaultValue, IFormatProvider format, IConversionObserver conversionObserver)
		{
			if ((value == null) || ConversionsHelper.IsDBNull(value))
			{
				// value is null or DBNull
				return defaultValue;
			}

			var sourceValueType = value.GetType();
			if (sourceValueType == null)
			{
				// unable to get the source value type
				return defaultValue;
			}

			var sourceValueTypeInfo = sourceValueType.GetTypeInfo();
			if (sourceValueTypeInfo == null)
			{
				// unable to get the source value type
				return defaultValue;
			}

			if ((sourceValueType != this.fromType) && (!this.fromTypeInfo.IsAssignableFrom(sourceValueTypeInfo)))
			{
				// the value cannot be converted to source type
				return defaultValue;
			}

			var targetDefaultValueType = this.toType;
			if (defaultValue == null)
			{
				if (this.toTypeInfo.IsValueType)
				{
					defaultValue = default(TTo);
				}
			}
			else
			{
				targetDefaultValueType = defaultValue.GetType();
				if (targetDefaultValueType == null)
				{
					targetDefaultValueType = this.toType;
					defaultValue = default(TTo);
				}
			}

			var defaultTargetValueTypeInfo = targetDefaultValueType.GetTypeInfo();
			if ((targetDefaultValueType != this.toType) && (!this.toTypeInfo.IsAssignableFrom(defaultTargetValueTypeInfo)))
			{
				targetDefaultValueType = this.toType;
				defaultValue = default(TTo);
			}

			return this.ConvertValue((TFrom)value, (TTo)defaultValue, format, conversionObserver);
		}
	}
}
