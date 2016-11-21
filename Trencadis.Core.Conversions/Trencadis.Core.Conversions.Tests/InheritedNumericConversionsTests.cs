using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Tests.Helpers.ImplicitConversions;

namespace Trencadis.Core.Conversions.Tests
{
	[TestFixture]
	public class InheritedNumericConversionsTests : NumericConversionsTests<int>
	{
		protected override int ChangedNumericDefault()
		{
			return -1;
		}

		protected override int TrueishBooleanValue()
		{
			return 1;
		}

		protected override int FalseishBooleanValue()
		{
			return 0;
		}

		protected override void String_representing_a_positive_numeric_value_setup(out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = 12;
			stringValue = correspondingNumericValue.ToString();
		}

		protected override void String_representing_a_negative_numeric_value_setup(out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = -12;
			stringValue = correspondingNumericValue.ToString();
		}

		protected override void String_with_group_separators_setup(IFormatProvider culture, out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = 123456;
			stringValue = correspondingNumericValue.ToString("N0", culture);
		}

		protected override void String_with_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = 123456;
			stringValue = correspondingNumericValue.ToString("N2", culture);
		}

		protected override void String_with_positive_sign_and_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = 123456;
			stringValue = "+" + correspondingNumericValue.ToString("N2", culture);
		}

		protected override void String_with_negative_sign_and_group_separators_and_decimal_point_setup(IFormatProvider culture, out string stringValue, out int correspondingNumericValue)
		{
			correspondingNumericValue = -123456;
			stringValue = correspondingNumericValue.ToString("N2", culture);
		}

		protected override void String_non_numeric(out string stringValue)
		{
			stringValue = "bla bla bla";
		}

		protected override void Enum_to_numeric_setup(out Enum enumValue, out int correspondingNumericValue)
		{
			enumValue = DayOfWeek.Monday;
			correspondingNumericValue = (int)DayOfWeek.Monday;
		}

		protected override void Float_without_decimal_part_to_numeric_setup(out float floatValue, out int correspondingNumericValue)
		{
			floatValue = 3.0F;
			correspondingNumericValue = 3;
		}

		protected override void Float_with_decimal_part_less_than_half_to_numeric_setup(out float floatValue, out int correspondingNumericValue)
		{
			floatValue = 3.25F;
			correspondingNumericValue = 3;
		}

		protected override void Float_with_decimal_part_greater_than_half_to_numeric_setup(out float floatValue, out int correspondingNumericValue)
		{
			floatValue = 3.65F;
			correspondingNumericValue = 3;
		}

		protected override void Float_with_decimal_part_exactly_half_to_numeric_setup(out float floatValue, out int correspondingNumericValue)
		{
			floatValue = 3.5F;
			correspondingNumericValue = 3;
		}

		protected override void Double_without_decimal_part_to_numeric_setup(out double doubleValue, out int correspondingNumericValue)
		{
			doubleValue = 3.0D;
			correspondingNumericValue = 3;
		}

		protected override void Double_with_decimal_part_less_than_half_to_numeric_setup(out double doubleValue, out int correspondingNumericValue)
		{
			doubleValue = 3.25D;
			correspondingNumericValue = 3;
		}

		protected override void Double_with_decimal_part_greater_than_half_to_numeric_setup(out double doubleValue, out int correspondingNumericValue)
		{
			doubleValue = 3.65D;
			correspondingNumericValue = 3;
		}

		protected override void Double_with_decimal_part_exactly_half_to_numeric_setup(out double doubleValue, out int correspondingNumericValue)
		{
			doubleValue = 3.5D;
			correspondingNumericValue = 3;
		}

		protected override void Decimal_without_decimal_part_to_numeric_setup(out decimal decimalValue, out int correspondingNumericValue)
		{
			decimalValue = 3.0M;
			correspondingNumericValue = 3;
		}

		protected override void Decimal_with_decimal_part_less_than_half_to_numeric_setup(out decimal decimalValue, out int correspondingNumericValue)
		{
			decimalValue = 3.25M;
			correspondingNumericValue = 3;
		}

		protected override void Decimal_with_decimal_part_greater_than_half_to_numeric_setup(out decimal decimalValue, out int correspondingNumericValue)
		{
			decimalValue = 3.65M;
			correspondingNumericValue = 3;
		}

		protected override void Decimal_with_decimal_part_exactly_half_to_numeric_setup(out decimal decimalValue, out int correspondingNumericValue)
		{
			decimalValue = 3.5M;
			correspondingNumericValue = 3;
		}

		protected override void Type_with_implicit_conversion_to_numeric_setup(out object implicitConvertibleInstance, out int correspondingNumericValue)
		{
			implicitConvertibleInstance = new ImplicitIntegerNumber(10);
			correspondingNumericValue = 10;
		}

		protected override void Type_with_explicit_conversion_to_numeric_setup(out object explicitConvertibleInstance, out int correspondingNumericValue)
		{
			explicitConvertibleInstance = new ImplicitIntegerNumber(10);
			correspondingNumericValue = 10;
		}
	}
}
