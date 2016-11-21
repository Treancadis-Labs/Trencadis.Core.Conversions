using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Tests.Helpers;

namespace Trencadis.Core.Conversions.Tests
{
	public partial class ConverterTests
	{
		[TestMethod]
		public void When_converting_null_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_null_to<short>(culture);
				NumericConversionsSpecs.When_converting_null_to<short>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_DBNull_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_DBNull_to<short>(culture);
				NumericConversionsSpecs.When_converting_DBNull_to<short>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_an_empty_string_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_empty_string_to<short>(culture);
				NumericConversionsSpecs.When_converting_an_empty_string_to<short>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_positive_integer_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>("1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_negative_integer_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>("-1", -1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_spaces_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>("  1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_tabs_to_short()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>("		1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_newlines_to_short()
		{
			var value = @"
			
			1";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_spaces_to_short()
		{
			var value = "1  ";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_tabs_to_short()
		{
			var value = "1		";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_newlines_to_short()
		{
			var value = @"1
			
			";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_to_short()
		{
			foreach (var culture in allCultures)
			{
				var value = 1234.ToString("N0", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1234, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_and_decimal_separator_to_short()
		{
			foreach (var culture in allCultures)
			{
				var value = 1234.ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, 1234, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_sign_and_group_separators_and_decimal_separator_to_short()
		{
			foreach (var culture in allCultures)
			{
				var value = (-1234).ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<short>(value, -1234, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_non_numeric_string_to_short()
		{
			foreach (var culture in allCultures)
			{
				var value = "test";
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<short, FormatException>(value, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<short, FormatException>(value, culture, -1);
			}
		}
	}
}
