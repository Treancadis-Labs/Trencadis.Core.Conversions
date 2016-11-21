using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trencadis.Core.Conversions.Tests.Helpers.ImplicitConversions
{
	public class ImplicitIntegerNumber
	{
		public int Value
		{
			get;
			set;
		}

		public ImplicitIntegerNumber()
			: this(default(int))
		{

		}

		public ImplicitIntegerNumber(int value)
		{
			this.Value = value;
		}

		public static implicit operator int(ImplicitIntegerNumber number)
		{
			if(number != null)
			{
				return number.Value;
			}

			return 0;
		}

		public static implicit operator ImplicitIntegerNumber(int number)
		{
			return new ImplicitIntegerNumber(number);
		}
	}
}
