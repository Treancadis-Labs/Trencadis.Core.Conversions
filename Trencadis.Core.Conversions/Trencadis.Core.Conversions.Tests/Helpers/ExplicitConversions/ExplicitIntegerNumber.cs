using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trencadis.Core.Conversions.Tests.Helpers.ExplicitConversions
{
	public class ExplicitIntegerNumber
	{
		public int Value
		{
			get;
			set;
		}

		public ExplicitIntegerNumber()
			: this(default(int))
		{

		}

		public ExplicitIntegerNumber(int value)
		{
			this.Value = value;
		}

		public static explicit operator int(ExplicitIntegerNumber number)
		{
			if(number != null)
			{
				return number.Value;
			}

			return 0;
		}

		public static explicit operator ExplicitIntegerNumber(int number)
		{
			return new ExplicitIntegerNumber(number);
		}
	}
}
