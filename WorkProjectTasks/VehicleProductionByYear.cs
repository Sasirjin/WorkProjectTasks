namespace WorkProjectTasks
{
    /// <summary>
    /// Defines the number of vehicles produced in a single year.
    /// </summary>
    public sealed class VehicleProductionByYear
    {
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets or sets the number of vehicles produced.
        /// </summary>
        public int Production { get; private set; }


        public VehicleProductionByYear(int year, int production)
        {
            Year = year;
            Production = production;
        }
    }
}