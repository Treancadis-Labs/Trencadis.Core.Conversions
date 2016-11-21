using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Globalization;
using Trencadis.Core.Conversions.Exceptions;
using Trencadis.Core.Conversions.Tests.Helpers;
using Trencadis.Core.Conversions.Tests.Helpers.ConversionObservers;
using Trencadis.Core.Conversions.Tests.Helpers.ExplicitConversions;
using Trencadis.Core.Conversions.Tests.Helpers.ImplicitConversions;

namespace Trencadis.Core.Conversions.Tests
{
	public partial class ConverterTests
	{

		#region null, DBNull.Value => int

		[TestMethod]
		public void When_converting_null_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_null_to<int>(culture);
				NumericConversionsSpecs.When_converting_null_to<int>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_null_to_int_with_null_default_value()
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = Converter.Convert(
				value: null,
				targetType: typeof(int),
				targetDefaultValue: null,
				format: null,
				conversionObserver: conversionObserver);

			Assert.AreEqual(0, result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOfType(conversionObserver.KnownFallbackToDefaultValueReason, typeof(ArgumentNullException));
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		[TestMethod]
		public void When_converting_DBNull_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_DBNull_to<int>(culture);
				NumericConversionsSpecs.When_converting_DBNull_to<int>(culture, -1);
			}
		}

		#endregion

		#region string => int

		[TestMethod]
		public void When_converting_an_empty_string_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_empty_string_to<int>(culture);
				NumericConversionsSpecs.When_converting_an_empty_string_to<int>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_positive_integer_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>("1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_negative_integer_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>("-1", -1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_spaces_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>("  1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_tabs_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>("		1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_newlines_to_int()
		{
			var value = @"
			
			1";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_spaces_to_int()
		{
			var value = "1  ";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_tabs_to_int()
		{
			var value = "1		";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_newlines_to_int()
		{
			var value = @"1
			
			";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_to_int()
		{
			foreach (var culture in allCultures)
			{
				var value = 123456.ToString("N0", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_and_decimal_separator_to_int()
		{
			foreach (var culture in allCultures)
			{
				var value = 123456.ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, 123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_sign_and_group_separators_and_decimal_separator_to_int()
		{
			foreach (var culture in allCultures)
			{
				var value = (-123456).ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<int>(value, -123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_non_numeric_string_to_int()
		{
			foreach (var culture in allCultures)
			{
				var value = "test";
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, FormatException>(value, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, FormatException>(value, culture, -1);
			}
		}

		#endregion

		#region bool => int

		[TestMethod]
		public void When_converting_a_true_boolean_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_boolean_to<int>(true, 1, culture);
				NumericConversionsSpecs.When_converting_a_boolean_to<int>(true, 1, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_false_boolean_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_boolean_to<int>(false, 0, culture);
				NumericConversionsSpecs.When_converting_a_boolean_to<int>(false, 0, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_nullable_true_boolean_to_int()
		{
			bool? value = true;
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, 1, culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, 1, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_nullable_false_boolean_to_int()
		{
			bool? value = false;
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, 0, culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, 0, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_nullable_null_boolean_to_int()
		{
			bool? value = null;
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, 0, culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<int>(value, -1, culture, -1);
			}
		}

		#endregion

		#region DataTable cell value => int

		[TestMethod]
		public void When_converting_a_table_row_column_to_int()
		{
			var table = new DataTable();
			table.Columns.AddRange(new DataColumn[]
			{
				new DataColumn("Col_Int", typeof(System.Int32)),
				new DataColumn("Col_Bool", typeof(System.Boolean)),
				new DataColumn("Col_Float", typeof(System.Boolean))
			});

			// 1
			table.Rows.Add(1, true, 1F);

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(table.Rows[0]["Col_Int"], 1, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(table.Rows[0]["Col_Int"], 1, culture, -1);

				// bool
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(table.Rows[0]["Col_Bool"], 1, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(table.Rows[0]["Col_Bool"], 1, culture, -1);
			}
		}

		#endregion

		#region Enum => int

		[TestMethod]
		public void When_converting_an_enum_value_to_int()
		{
			var enumValue = DayOfWeek.Monday;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(enumValue, 1, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(enumValue, 1, culture, -1);
			}
		}

		#endregion

		#region float => int

		[TestMethod]
		public void When_converting_a_float_value_without_decimal_part_to_int()
		{
			var floatValue = 3.0F;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_float_value_with_decimal_part_less_than_half_truncate_expected()
		{
			var floatValue = 3.25F;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_float_value_with_decimal_part_greater_than_half_truncate_expected()
		{
			var floatValue = 3.65F;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_float_value_with_decimal_part_exactly_to_half_up_truncate_expected()
		{
			var floatValue = 3.5F;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(floatValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_positive_infinity_float_value_to_int()
		{
			var floatValue = float.PositiveInfinity;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(floatValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(floatValue, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_negative_infinity_float_value_to_int()
		{
			var floatValue = float.NegativeInfinity;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(floatValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(floatValue, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_NaN_float_value_to_int()
		{
			var floatValue = float.NaN;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, InvalidCastException>(floatValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, InvalidCastException>(floatValue, culture, -1);
			}
		}

		#endregion

		#region double => int

		[TestMethod]
		public void When_converting_a_double_value_without_decimal_part_to_int()
		{
			var doubleValue = 3.0D;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_double_value_with_decimal_part_less_than_half_truncate_expected()
		{
			var doubleValue = 3.25D;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_double_value_with_decimal_part_greater_than_half_truncate_expected()
		{
			var doubleValue = 3.65D;

			// .NET Weirdness !!!
			Assert.AreEqual(3, (int)doubleValue);
			Assert.AreEqual(4, Convert.ChangeType(doubleValue, typeof(int)));

			// TODO: investigate why my code gives different results
			// http://stackoverflow.com/questions/13292148/casting-a-double-as-an-int-does-it-round-or-just-strip-digits

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_double_value_with_decimal_part_exactly_to_half_truncate_expected()
		{
			var doubleValue = 3.5D;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(doubleValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_positive_infinity_double_value_to_int()
		{
			var doubleValue = double.PositiveInfinity;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(doubleValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(doubleValue, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_negative_infinity_double_value_to_int()
		{
			var doubleValue = double.NegativeInfinity;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(doubleValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(doubleValue, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_NaN_double_value_to_int()
		{
			var doubleValue = double.NaN;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, InvalidCastException>(doubleValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, InvalidCastException>(doubleValue, culture, -1);
			}
		}

		#endregion

		#region decimal => int

		[TestMethod]
		public void When_converting_a_decimal_value_without_decimal_part_to_int()
		{
			var decimalValue = 3.0M;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_decimal_value_with_decimal_part_less_than_half_to_int_down_truncate_expected()
		{
			var decimalValue = 3.25M;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_decimal_value_with_decimal_part_greater_than_half_to_int_up_truncate_expected()
		{
			var decimalValue = 3.65M;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_decimal_value_with_decimal_part_exactly_to_half_up_truncate_expected()
		{
			var decimalValue = 3.5M;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(decimalValue, 3, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_decimal_max_value_to_int()
		{
			var decimalValue = decimal.MaxValue;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(decimalValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(decimalValue, culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_decimal_min_value_to_int()
		{
			var decimalValue = decimal.MinValue;

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(decimalValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<int, OverflowException>(decimalValue, culture, -1);
			}
		}

		#endregion

		#region Usage of implicit conversion operators

		[TestMethod]
		public void When_converting_to_int_from_a_type_which_has_implicit_conversion_operator()
		{
			var value = new ImplicitIntegerNumber(10);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(value, 10, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(value, 10, culture, -1);
			}
		}

		#endregion

		#region Usage of explicit conversion operators

		[TestMethod]
		public void When_converting_to_int_from_a_type_which_has_explicit_conversion_operator()
		{
			var value = new ExplicitIntegerNumber(12);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(value, 12, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<int>(value, 12, culture, -1);
			}
		}

		#endregion
	}
}
