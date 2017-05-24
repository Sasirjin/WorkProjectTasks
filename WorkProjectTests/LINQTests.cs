namespace WorkProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WorkProjectTasks;

    [TestClass]
    public class LINQTests
    {
        private VehicleProductionByCountry[] VehicleProductionByCountry;

        [TestMethod]
        public void FindYearWithTotalHighestNumberOfVehiclesProduced()
        {
            Assert.AreEqual(LINQTasks.FindYearWithTotalHighestNumberOfVehiclesProduced(VehicleProductionByCountry).Year, 2013);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindYearWithTotalHighestNumberOfVehiclesProducedWithEmptyArray()
        {
            LINQTasks.FindYearWithTotalHighestNumberOfVehiclesProduced(new VehicleProductionByCountry[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindYearWithTotalHighestNumberOfVehiclesProducedWithNullARgument()
        {
            LINQTasks.FindYearWithTotalHighestNumberOfVehiclesProduced(null);
        }

        [TestMethod]
        public void FindTheAverageNumberOfVehiclesProducedByCountryPerYear()
        {
            Assert.AreEqual(LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(2013, VehicleProductionByCountry), 1595848);
            Assert.AreEqual(LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(2012, VehicleProductionByCountry), 1485231);
            Assert.AreEqual(LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(2011, VehicleProductionByCountry), 1409963);
            Assert.AreEqual(LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(2010, VehicleProductionByCountry), 1345553);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FindTheAverageNumberOfVehiclesProducedByCountryPerYearWithUnknownYear()
        {
            LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(1900, VehicleProductionByCountry);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindTheAverageNumberOfVehiclesProducedByCountryPerYearWithNullArgument()
        {
            LINQTasks.FindTheAverageNumberOfVehiclesProducedByCountryPerYear(0, null);
        }

        [TestInitialize]
        public void Initialize()
        {
            string line;
            string[] columns;
            int[] years = null;

            List<VehicleProductionByCountry> vehicleProductionByCountry = new List<VehicleProductionByCountry>();

            using (Stream stream = File.OpenRead("CountryVehicleProduction.csv"))
            using (StreamReader reader = new StreamReader(stream))
            {
                for (int i = 0; ; i++)
                {
                    line = reader.ReadLine();

                    if (line == null)
                        break; //eof

                    columns = line.Split(',');

                    if (i == 0) //headers: parse only years
                        years = columns
                            .Skip(1)
                            .Select(x => int.Parse(x))
                            .ToArray();
                    else //project into production by year and cull null or empty values 
                        vehicleProductionByCountry.Add(
                            new VehicleProductionByCountry(columns[0],
                                columns
                                .Skip(1)
                                .Select((production, index) => new { year = years[index], production = production })
                                .Where(x => !string.IsNullOrWhiteSpace(x.production))
                                .Select(x => new VehicleProductionByYear(x.year, int.Parse(x.production))).ToArray()));
                }
            }

            VehicleProductionByCountry = vehicleProductionByCountry.ToArray();
        }
    }
}
