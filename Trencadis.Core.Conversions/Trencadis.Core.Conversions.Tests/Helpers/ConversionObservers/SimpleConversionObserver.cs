using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trencadis.Core.Conversions.Infrastructure.ConversionObserver;

namespace Trencadis.Core.Conversions.Tests.Helpers.ConversionObservers
{
	public class SimpleConversionObserver : IConversionObserver
	{
		public bool KnownFallbackToDefaultValueOccured
		{
			get;
			private set;
		}

		public Exception KnownFallbackToDefaultValueReason
		{
			get;
			private set;
		}

		public bool CaughtConversionExceptionOccured
		{
			get;
			private set;
		}

		public Exception CaughtConversionException
		{
			get;
			private set;
		}

		public void NotifyKnownFallbackToDefaultValue(object value, Type targetType, object defaultTargetValue, IFormatProvider format, Exception fallbackReason)
		{
			this.KnownFallbackToDefaultValueOccured = true;
			this.KnownFallbackToDefaultValueReason = fallbackReason;
		}

		public void NotifyCaughtConversionException(object value, Type targetType, object defaultTargetValue, IFormatProvider format, Exception conversionException)
		{
			this.CaughtConversionExceptionOccured = true;
			this.CaughtConversionException = conversionException;
		}
	}
}
