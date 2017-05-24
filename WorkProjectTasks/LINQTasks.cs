namespace WorkProjectTasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LINQTasks
    {
        /// <summary>
        /// Find the year with the total highest number of vehicles produced in the world using LINQ.
        /// </summary>
        /// <param name="countryVehicleProductionByYear">Any array of vehicle production by country instances.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">The collection <paramref name="countryVehicleProductionByYear"/> cannot be null.</exception>
        /// <exception cref="ArgumentException">The collection <paramref name="countryVehicleProductionByYear"/> cannot be empty.</exception>
        public static VehicleProductionByYear FindYearWithTotalHighestNumberOfVehiclesProduced(VehicleProductionByCountry[] countryVehicleProductionByYear)
        {
            // Test input parameter
            if (countryVehicleProductionByYear == null)
            {
                throw new ArgumentNullException("countryVehicleProductionByYear", "The collection countryVehicleProductionByYear cannot be null.");
            }

            if (countryVehicleProductionByYear.Length == 0)
            {
                throw new ArgumentException("The collection countryVehicleProductionByYear cannot be empty.", "countryVehicleProductionByYear");
            }

            // Select out annual values
            var allYearlyProduction = countryVehicleProductionByYear
                .SelectMany(country => country.Production);

            // Group and sum into new VehicleProductionByYear
            var results = allYearlyProduction
                .GroupBy(p => p.Year)
                .Select(p => new VehicleProductionByYear(p.First().Year, p.Sum(a => a.Production)));

            // Return first in order of highest production
            return results.OrderByDescending(p => p.Production).FirstOrDefault();
        }

        /// <summary>
        /// Find the average number (rounded up) of vehicles produced by country for the specified <paramref name="year"/> using LINQ.
        /// </summary>
        /// <param name="year">The year to average production values for.</param>
        /// <param name="countryVehicleProductionByYear">Any array of vehicle production by country instances.</param>
        /// <returns>The average number of vehicles produced by vehicle producing countries.</returns>
        /// <exception cref="ArgumentNullException">The collection <paramref name="countryVehicleProductionByYear"/> cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="year"/> specified yields no data.</exception>
        /// <remarks>Average includes only vehicle producing countries. If a country 
        /// did not report any production values, it is excluded from the result. The average is rounded up.</remarks>
        public static int FindTheAverageNumberOfVehiclesProducedByCountryPerYear(int year, VehicleProductionByCountry[] countryVehicleProductionByYear)
        {
            // Test input parameter
            if (countryVehicleProductionByYear == null)
            {
                throw new ArgumentNullException("countryVehicleProductionByYear", "The collection countryVehicleProductionByYear cannot be null.");
            }

            // Should also, but was not asked to, include a parameter check as above does
            //if (countryVehicleProductionByYear.Length == 0)
            //{
            //    throw new ArgumentException("The collection countryVehicleProductionByYear cannot be empty.", "countryVehicleProductionByYear");
            //}

            // Select reporting countries
            var reportingCountries = countryVehicleProductionByYear
                .SelectMany(country => country.Production, (cnty, prod) => new { cnty.CountryName, prod.Production, prod.Year })
                .Where(prod => prod.Year == year);

            // Get the numbers
            var numberOfCountries = reportingCountries.Count();
            var totalProduction = reportingCountries.Sum(prod => prod.Production);
            var results = 0;

            // Test the output
            if (numberOfCountries == 0 || totalProduction == 0)
            {
                throw new ArgumentOutOfRangeException("The year specified yields no data.", "year");
            }
            else
            {
                decimal output = (totalProduction / numberOfCountries);
                results = (int)Math.Ceiling(output);

                if (results < 1)
                {
                    throw new ArgumentOutOfRangeException("The year specified yields no data.", "year");
                }
            }

            return results;
        }
    }
}