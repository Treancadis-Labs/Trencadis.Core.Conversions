// ---------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecializedConversionsBootstrapper.cs" company="Trencadis">
// Copyright (c) 2016, Trencadis, All rights reserved
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------------------------

namespace Trencadis.Core.Conversions.Infrastructure.Bootstrapping
{
	using System.Collections.Generic;

	using Trencadis.Core.Conversions.SpecializedConversions;

	/// <summary>
	/// Class responsible for the descovery and initialization of the known <see cref="ISpecializedConverter"/>(s)
	/// </summary>
	public abstract class SpecializedConversionsBootstrapper : ISpecializedConversionsBootstrapper
	{
		/// <summary>
		/// Holds the list of discovered specialized converters
		/// </summary>
		private readonly List<ISpecializedConverter> discoveredSpecializedConverters;

		/// <summary>
		/// Initializes a new instace of the <see cref="SpecializedConversionsBootstrapper"/> class
		/// </summary>
		public SpecializedConversionsBootstrapper()
		{
			this.discoveredSpecializedConverters = new List<ISpecializedConverter>();
		}

		/// <summary>
		/// Holds the collection of discovered <see cref="ISpecializedConverter"/>(s)
		/// </summary>
		public virtual IEnumerable<ISpecializedConverter> DiscoveredSpecializedConverters
		{
			get
			{
				if (this.discoveredSpecializedConverters.Count == 0)
				{
					this.DiscoverConverters();
				}

				return this.discoveredSpecializedConverters;
			}
		}

		/// <summary>
		/// Override to implement the logic for discovery and initialization of the known specialized converters
		/// </summary>
		protected abstract void DiscoverConverters();

		/// <summary>
		/// Adds a known specialized converter to the collection of discovered converters
		/// </summary>
		/// <typeparam name="TConverter"></typeparam>
		protected void AddSpecializedConverter<TConverter>()
			where TConverter : ISpecializedConverter, new()
		{
			this.discoveredSpecializedConverters.Add(new TConverter());
		}
	}
}
