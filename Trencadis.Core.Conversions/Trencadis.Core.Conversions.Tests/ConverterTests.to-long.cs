using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Tests.Helpers;

namespace Trencadis.Core.Conversions.Tests
{
	public partial class ConverterTests
	{
		[TestMethod]
		public void When_converting_null_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_null_to<long>(culture);
				NumericConversionsSpecs.When_converting_null_to<long>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_DBNull_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_DBNull_to<long>(culture);
				NumericConversionsSpecs.When_converting_DBNull_to<long>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_an_empty_string_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_an_empty_string_to<long>(culture);
				NumericConversionsSpecs.When_converting_an_empty_string_to<long>(culture, -1);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_positive_integer_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>("1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_representing_a_negative_integer_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>("-1", -1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_spaces_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>("  1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_tabs_to_long()
		{
			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>("		1", 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_leading_newlines_to_long()
		{
			var value = @"
			
			1";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_spaces_to_long()
		{
			var value = "1  ";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_tabs_to_long()
		{
			var value = "1		";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_trailing_newlines_to_long()
		{
			var value = @"1
			
			";

			foreach (var culture in allCultures)
			{
				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 1, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_to_long()
		{
			foreach (var culture in allCultures)
			{
				var value = 123456.ToString("N0", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_group_separators_and_decimal_separator_to_long()
		{
			foreach (var culture in allCultures)
			{
				var value = 123456.ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, 123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_string_with_sign_and_group_separators_and_decimal_separator_to_long()
		{
			foreach (var culture in allCultures)
			{
				var value = (-123456).ToString("N2", culture);

				NumericConversionsSpecs.When_converting_a_string_representing_a_number_to<long>(value, -123456, culture);
			}
		}

		[TestMethod]
		public void When_converting_a_non_numeric_string_to_long()
		{
			foreach (var culture in allCultures)
			{
				var value = "test";
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<long, FormatException>(value, culture);
				NumericConversionsSpecs.When_converting_an_object_with_default_fallback_to<long, FormatException>(value, culture, -1);
			}
		}
	}
}
