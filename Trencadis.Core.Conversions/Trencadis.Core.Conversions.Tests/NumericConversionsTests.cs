using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Tests.Helpers;
using Trencadis.Core.Conversions.Tests.Helpers.ConversionObservers;
using Trencadis.Core.Conversions.Tests.Helpers.ExplicitConversions;
using Trencadis.Core.Conversions.Tests.Helpers.ImplicitConversions;

namespace Trencadis.Core.Conversions.Tests
{
	[TestFixture]
	public abstract class NumericConversionsTests<TNumeric>
		where TNumeric: struct
	{
		protected static readonly CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

		#region Numeric defaults

		protected abstract TNumeric ChangedNumericDefault();

		protected abstract TNumeric TrueishBooleanValue();

		protected abstract TNumeric FalseishBooleanValue();

		#endregion

		#region Tests setup

		protected abstract void String_representing_a_positive_numeric_value_setup(out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_representing_a_negative_numeric_value_setup(out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_with_group_separators_setup(IFormatProvider culture, out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_with_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_with_positive_sign_and_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_with_negative_sign_and_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out TNumeric correspondingNumericValue);

		protected abstract void String_non_numeric(out string stringValue);

		protected abstract void Enum_to_numeric_setup(out Enum enumValue, out TNumeric correspondingNumericValue);


		protected abstract void Float_without_decimal_part_to_numeric_setup(out float floatValue, out TNumeric correspondingNumericValue);

		protected abstract void Float_with_decimal_part_less_than_half_to_numeric_setup(out float floatValue, out TNumeric correspondingNumericValue);

		protected abstract void Float_with_decimal_part_greater_than_half_to_numeric_setup(out float floatValue, out TNumeric correspondingNumericValue);

		protected abstract void Float_with_decimal_part_exactly_half_to_numeric_setup(out float floatValue, out TNumeric correspondingNumericValue);


		protected abstract void Double_without_decimal_part_to_numeric_setup(out double doubleValue, out TNumeric correspondingNumericValue);

		protected abstract void Double_with_decimal_part_less_than_half_to_numeric_setup(out double doubleValue, out TNumeric correspondingNumericValue);

		protected abstract void Double_with_decimal_part_greater_than_half_to_numeric_setup(out double doubleValue, out TNumeric correspondingNumericValue);

		protected abstract void Double_with_decimal_part_exactly_half_to_numeric_setup(out double doubleValue, out TNumeric correspondingNumericValue);


		protected abstract void Decimal_without_decimal_part_to_numeric_setup(out decimal decimalValue, out TNumeric correspondingNumericValue);

		protected abstract void Decimal_with_decimal_part_less_than_half_to_numeric_setup(out decimal decimalValue, out TNumeric correspondingNumericValue);

		protected abstract void Decimal_with_decimal_part_greater_than_half_to_numeric_setup(out decimal decimalValue, out TNumeric correspondingNumericValue);

		protected abstract void Decimal_with_decimal_part_exactly_half_to_numeric_setup(out decimal decimalValue, out TNumeric correspondingNumericValue);


		protected abstract void Type_with_implicit_conversion_to_numeric_setup(out object implicitConvertibleInstance, out TNumeric correspondingNumericValue);

		protected abstract void Type_with_explicit_conversion_to_numeric_setup(out object explicitConvertibleInstance, out TNumeric correspondingNumericValue);

		#endregion

		#region null, DBNull.Value => int

		[Test]
		public void When_converting_null_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_null_to<TNumeric>(culture);
				NumericConversionsSpecs.When_converting_null_to<TNumeric>(culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_null_to_numeric_with_null_default_value()
		{
			var conversionObserver = new SimpleConversionObserver();
			var result = Converter.Convert(
				value: null,
				targetType: typeof(TNumeric),
				targetDefaultValue: null,
				format: null,
				conversionObserver: conversionObserver);

			Assert.AreEqual(default(TNumeric), result);
			Assert.IsTrue(conversionObserver.KnownFallbackToDefaultValueOccured);
			Assert.IsInstanceOf<ArgumentNullException>(conversionObserver.KnownFallbackToDefaultValueReason);
			Assert.IsFalse(conversionObserver.CaughtConversionExceptionOccured);
			Assert.IsNull(conversionObserver.CaughtConversionException);
		}

		[Test]
		public void When_converting_DBNull_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_DBNull_to<TNumeric>(culture);
				NumericConversionsSpecs.When_converting_DBNull_to<TNumeric>(culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region string => int

		[Test]
		public void When_converting_an_empty_string_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_empty_string_to<TNumeric>(culture);
				NumericConversionsSpecs.When_converting_an_empty_string_to<TNumeric>(culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_string_representing_a_positive_value_to_numeric()
		{
			string stringVal;
			TNumeric numericVal;
			this.String_representing_a_positive_numeric_value_setup(out stringVal, out numericVal);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_representing_a_negative_value_to_numeric()
		{
			string stringVal;
			TNumeric numericVal;
			this.String_representing_a_negative_numeric_value_setup(out stringVal, out numericVal);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_leading_spaces_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string leadingSpacesPosStringVal = "  " + posStringVal;

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string leadingSpacesNegStringVal = "   " + negStringVal;

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingSpacesPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingSpacesNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_leading_tabs_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string leadingTabsPosStringVal = "		" + posStringVal;

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string leadingTabsNegStringVal = "			" + negStringVal;

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingTabsPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingTabsNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_leading_newlines_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string leadingNewlinesPosStringVal = @"


				" + posStringVal;

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string leadingNewlinesNegStringVal = @"

			
			
			" + negStringVal;

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingNewlinesPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(leadingNewlinesNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_trailing_spaces_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string trailingSpacesPosStringVal = posStringVal + "  ";

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string trailingSpacesNegStringVal = negStringVal + "   ";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingSpacesPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingSpacesNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_trailing_tabs_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string trailingTabsPosStringVal = posStringVal + "		";

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string trailingTabsNegStringVal = negStringVal + "			";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingTabsPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingTabsNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_trailing_newlines_to_numeric()
		{
			string posStringVal;
			TNumeric posNumericVal;
			this.String_representing_a_positive_numeric_value_setup(out posStringVal, out posNumericVal);
			string trailingNewlinesPosStringVal = posStringVal + @"


				";

			string negStringVal;
			TNumeric negNumericVal;
			this.String_representing_a_negative_numeric_value_setup(out negStringVal, out negNumericVal);
			string trailingNewlinesNegStringVal = negStringVal + @"

			
			
			";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingNewlinesPosStringVal, posNumericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(trailingNewlinesNegStringVal, negNumericVal, culture);
			}
		}

		[Test]
		public void When_converting_a_string_with_group_separators_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				string stringVal;
				TNumeric numericVal;
				this.String_with_group_separators_setup(culture, out stringVal, out numericVal);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_string_with_group_separators_and_decimal_separator_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				string stringVal;
				TNumeric numericVal;
				this.String_with_group_separators_and_decimal_point_setup(culture, out stringVal, out numericVal);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_string_with_positive_sign_and_group_separators_and_decimal_separator_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				string stringVal;
				TNumeric numericVal;
				this.String_with_positive_sign_and_group_separators_and_decimal_point_setup(culture, out stringVal, out numericVal);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_string_with_negative_sign_and_group_separators_and_decimal_separator_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				string stringVal;
				TNumeric numericVal;
				this.String_with_negative_sign_and_group_separators_and_decimal_point_setup(culture, out stringVal, out numericVal);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture);
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<TNumeric>(stringVal, numericVal, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_non_numeric_string_to_numeric()
		{
			string stringVal;
			this.String_non_numeric(out stringVal);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, FormatException>(stringVal, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, FormatException>(stringVal, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region bool => int

		[Test]
		public void When_converting_a_true_boolean_to_numeric()
		{
			var trueishValue = this.TrueishBooleanValue();

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_boolean_to<TNumeric>(true, trueishValue, culture);
				NumericConversionsSpecs.When_converting_a_boolean_to<TNumeric>(true, trueishValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_false_boolean_to_numeric()
		{
			var falseishValue = this.FalseishBooleanValue();

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_boolean_to<TNumeric>(false, falseishValue, culture);
				NumericConversionsSpecs.When_converting_a_boolean_to<TNumeric>(false, falseishValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_nullable_true_boolean_to_numeric()
		{
			var trueishValue = this.TrueishBooleanValue();
			bool? nullableTrue = true;

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullableTrue, trueishValue, culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullableTrue, trueishValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_nullable_false_boolean_to_numeric()
		{
			var falseishValue = this.FalseishBooleanValue();
			bool? nullableFalse = false;

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullableFalse, falseishValue, culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullableFalse, falseishValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_nullable_null_boolean_to_numeric()
		{
			bool? nullBoolean = null;
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullBoolean, default(TNumeric), culture);
				NumericConversionsSpecs.When_converting_a_nullable_boolean_to<TNumeric>(nullBoolean, this.ChangedNumericDefault(), culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region Enum => int

		[Test]
		public void When_converting_an_enum_value_to_int()
		{
			Enum enumValue;
			TNumeric numericValue;
			this.Enum_to_numeric_setup(out enumValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(enumValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(enumValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region float => int

		[Test]
		public void When_converting_a_float_value_without_decimal_part_to_numeric()
		{
			float floatValue;
			TNumeric numericValue;
			this.Float_without_decimal_part_to_numeric_setup(out floatValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_float_value_with_decimal_part_less_than_half_truncate_expected()
		{
			float floatValue;
			TNumeric numericValue;
			this.Float_with_decimal_part_less_than_half_to_numeric_setup(out floatValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_float_value_with_decimal_part_greater_than_half_truncate_expected()
		{
			float floatValue;
			TNumeric numericValue;
			this.Float_with_decimal_part_less_than_half_to_numeric_setup(out floatValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_float_value_with_decimal_part_exactly_to_half_up_truncate_expected()
		{
			float floatValue;
			TNumeric numericValue;
			this.Float_with_decimal_part_exactly_half_to_numeric_setup(out floatValue, out numericValue);

			foreach (var culture in allCultures)
			{
				// integer
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(floatValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_positive_infinity_float_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(float.PositiveInfinity, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(float.PositiveInfinity, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_negative_infinity_float_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(float.NegativeInfinity, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(float.NegativeInfinity, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_NaN_float_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, InvalidCastException>(float.NaN, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, InvalidCastException>(float.NaN, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region double => int

		[Test]
		public void When_converting_a_double_value_without_decimal_part_to_numeric()
		{
			double doubleValue;
			TNumeric numericValue;
			this.Double_without_decimal_part_to_numeric_setup(out doubleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_double_value_with_decimal_part_less_than_half_truncate_expected()
		{
			double doubleValue;
			TNumeric numericValue;
			this.Double_with_decimal_part_less_than_half_to_numeric_setup(out doubleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_double_value_with_decimal_part_greater_than_half_truncate_expected()
		{
			double doubleValue;
			TNumeric numericValue;
			this.Double_with_decimal_part_less_than_half_to_numeric_setup(out doubleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_double_value_with_decimal_part_exactly_to_half_truncate_expected()
		{
			double doubleValue;
			TNumeric numericValue;
			this.Double_with_decimal_part_exactly_half_to_numeric_setup(out doubleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(doubleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_positive_infinity_double_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(double.PositiveInfinity, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(double.PositiveInfinity, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_negative_infinity_double_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(double.NegativeInfinity, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(double.NegativeInfinity, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_NaN_double_value_to_numeric()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, InvalidCastException>(double.NaN, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, InvalidCastException>(double.NaN, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region decimal => int

		[Test]
		public void When_converting_a_decimal_value_without_decimal_part_to_numeric()
		{
			decimal decimalValue;
			TNumeric numericValue;
			this.Decimal_without_decimal_part_to_numeric_setup(out decimalValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_decimal_value_with_decimal_part_less_than_half_to_int_down_truncate_expected()
		{
			decimal decimalValue;
			TNumeric numericValue;
			this.Decimal_with_decimal_part_less_than_half_to_numeric_setup(out decimalValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_decimal_value_with_decimal_part_greater_than_half_to_int_up_truncate_expected()
		{
			decimal decimalValue;
			TNumeric numericValue;
			this.Decimal_with_decimal_part_less_than_half_to_numeric_setup(out decimalValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_decimal_value_with_decimal_part_exactly_to_half_up_truncate_expected()
		{
			decimal decimalValue;
			TNumeric numericValue;
			this.Decimal_with_decimal_part_exactly_half_to_numeric_setup(out decimalValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(decimalValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_decimal_max_value_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(decimal.MaxValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(decimal.MaxValue, culture, this.ChangedNumericDefault());
			}
		}

		[Test]
		public void When_converting_a_decimal_min_value_to_int()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(decimal.MinValue, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<TNumeric, OverflowException>(decimal.MinValue, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region Usage of implicit conversion operators

		[Test]
		public void When_converting_to_int_from_a_type_which_has_implicit_conversion_operator()
		{
			object implicitConvertibleValue;
			TNumeric numericValue;
			this.Type_with_implicit_conversion_to_numeric_setup(out implicitConvertibleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(implicitConvertibleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(implicitConvertibleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		#endregion

		#region Usage of explicit conversion operators

		[Test]
		public void When_converting_to_int_from_a_type_which_has_explicit_conversion_operator()
		{
			object explicitConvertibleValue;
			TNumeric numericValue;
			this.Type_with_explicit_conversion_to_numeric_setup(out explicitConvertibleValue, out numericValue);

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(explicitConvertibleValue, numericValue, culture);
				NumericConversionsSpecs.When_converting_an_object_without_default_fallback_to<TNumeric>(explicitConvertibleValue, numericValue, culture, this.ChangedNumericDefault());
			}
		}

		#endregion
	}
}
