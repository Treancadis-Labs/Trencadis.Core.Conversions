using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Exceptions;
using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;
using Trencadis.Core.Conversions.Tests.Helpers.ConversionObservers;

namespace Trencadis.Core.Conversions.Tests.Helpers
{
	public static class NumericConversionsSpecs
	{
		public static void When_converting_null_to<TNumeric>(IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			object nullValue = null;
			var result = nullValue.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(defaultValue, result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(ArgumentNullException));
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_DBNull_to<TNumeric>(IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			var dbNullValue = DBNull.Value;
			var result = dbNullValue.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(defaultValue, result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(ArgumentNullException));
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_an_empty_string_to<TNumeric>(IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var value = "";
			var conversionObserver = new SimpleConversionObserver();
			var result = value.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(defaultValue, result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(ArgumentNullException)); // or maybe a FormatException??
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_a_string_representing_a_number_to<TNumeric>(string numberAsString, TNumeric expectedResult, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = numberAsString.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(expectedResult, result);
			Assert.IsFalse(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsNull(conversionObserver.KnownFallbackToDefaultValueReason);
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_a_boolean_to<TNumeric>(bool booleanValue, TNumeric expectedResult, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = booleanValue.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(expectedResult, result);
			Assert.IsFalse(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsNull(conversionObserver.KnownFallbackToDefaultValueReason);
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_a_nullable_boolean_to<TNumeric>(bool? booleanValue, TNumeric expectedResult, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = booleanValue.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(expectedResult, result);
			if(booleanValue.HasValue)
			{
				Assert.IsFalse(conversionObserver.KnownFallbackToDefaultValueOccured);
				Assert.IsNull(conversionObserver.KnownFallbackToDefaultValueReason);
			}
			else
			{
				Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
				Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(ArgumentNullException));
			}
			
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_an_object_without_default_fallback_to<TNumeric>(object value, TNumeric expectedResult, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = value.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(expectedResult, result);
			Assert.IsFalse(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsNull(conversionObserver.KnownFallbackToDefaultValueReason);
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_an_object_with_default_fallback_to<TNumeric, TFallbackReason>(object value, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
			where TFallbackReason: Exception
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = value.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(defaultValue, result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(TFallbackReason));
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		public static void When_converting_an_object_with_exception_to<TNumeric, TException>(object value, IFormatProvider format, TNumeric defaultValue = default(TNumeric))
			where TNumeric : struct
			where TException : Exception
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = value.ConvertTo<TNumeric>(
				defaultValue: defaultValue,
				format: format,
				conversionObserver: conversionObserver);

			Assert.AreEqual(defaultValue, result);
			Assert.IsFalse(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsNull(conversionObserver.KnownFallbackToDefaultValueReason);
			Assert.IsTrue(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsInstanceOfType(conversionObserver.CaughtConversionException, typeof(TException));
		}
	}
}
