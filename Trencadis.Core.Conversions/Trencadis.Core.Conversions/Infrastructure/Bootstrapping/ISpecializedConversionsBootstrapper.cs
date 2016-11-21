// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecializedConversionsBootstrapper.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Infrastructure.Bootstrapping
{
	using System.Collections.Generic;

	using Trencadis.Core.Conversions.SpecializedConversions;

	/// <summary>
	/// Abstracts a class capable of discovery and initialization of the <see cref="ISpecializedConverter"/>(s)
	/// </summary>
	public interface ISpecializedConversionsBootstrapper
	{
		/// <summary>
		/// Gets the collection of specialized converters
		/// </summary>
		IEnumerable<ISpecializedConverter> DiscoveredSpecializedConverters
		{
			get;
		}
	}
}
