namespace WorkProjectTasks
{
    using System;

    /// <summary>
    /// Defines the vehicle production numbers for a country.
    /// </summary>
    public sealed class VehicleProductionByCountry
    {
        /// <summary>
        /// The country vehicles were produced in.
        /// </summary>
        public string CountryName { get; private set; }

        /// <summary>
        /// An array of yearly vehicle production numbers.
        /// </summary>
        public VehicleProductionByYear[] Production { get; private set; }

        public VehicleProductionByCountry(string countryName, VehicleProductionByYear[] production)
        {
            if (countryName == null)
                throw new ArgumentNullException("countryName");

            if (production == null)
                throw new ArgumentNullException("production");

            CountryName = countryName;
            Production = production;
        }
    }
}