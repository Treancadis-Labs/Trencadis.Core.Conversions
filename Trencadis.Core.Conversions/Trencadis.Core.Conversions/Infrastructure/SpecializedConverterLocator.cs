// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecializedConverterLocator.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Infrastructure
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Trencadis.Core.Conversions.SpecializedConversions;

	/// <summary>
	/// Class can resolve specialized converters for the provided arguments
	/// </summary>
	public static class SpecializedConverterLocator
	{
		/// <summary>
		/// Gets the specialized converter for the given type conversion
		/// </summary>
		/// <param name="from">The type from which we try to convert</param>
		/// <param name="to">The type to which we try to convert</param>
		/// <param name="knownSpecializedConverters">The collection of known specialized converters</param>
		/// <returns>The matching specialized converter if found, null otherwise</returns>
		public static ISpecializedConverter GetSpecializedConverter(Type from, Type to, IEnumerable<ISpecializedConverter> knownSpecializedConverters)
		{
			if ((from != null) && (to != null) && (knownSpecializedConverters != null))
			{
				var converter = knownSpecializedConverters.FirstOrDefault(conv => (conv.FromType == from) && (conv.ToType == to));

				return converter;
			}

			return null;
		}
	}
}
