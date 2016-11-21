// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConversionException.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Exceptions
{
	using System;

	/// <summary>
	/// Represents a type conversion exception
	/// </summary>
	public class TypeConversionException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeConversionException"/> class
		/// </summary>
		/// <param name="message">The error message</param>
		/// <param name="innerException">The inner exception (the original conversion exception)</param>
		/// <param name="value">The value that we tried to convert</param>
		/// <param name="defaultValue">The default value to use for null or DBNull</param>
		/// <param name="targetType">The conversion target type</param>
		/// <param name="formatProvider">The format provider</param>
		public TypeConversionException(string message, Exception innerException, object value = null, object defaultValue = null, Type targetType = null, IFormatProvider formatProvider = null)
			: base(message, innerException)
		{
			this.Value = value;
			this.DefaultValue = defaultValue;
			this.TargetType = targetType;
			this.FormatProvider = formatProvider;
		}

		/// <summary>
		/// Gets the value that we tried to convert
		/// </summary>
		public object Value
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the default value set to be used when converting null or DBNull value(s)
		/// </summary>
		public object DefaultValue
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the target type
		/// </summary>
		public Type TargetType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the format provider
		/// </summary>
		public IFormatProvider FormatProvider
		{
			get;
			private set;
		}
	}
}
