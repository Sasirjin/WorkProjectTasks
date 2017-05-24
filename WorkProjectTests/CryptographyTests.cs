namespace WorkProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using WorkProjectTasks;

    [TestClass]
    public class CryptographyTests
    {
        [TestMethod]
        public void ComputeSHA256()
        {
            Assert.AreEqual(CryptographyTasks.CalculateSHA256Hash("SHA-256 HASHED"), "c88ba9717a37ae6110fdf313cac8356bc6ef29cd5cb9bb6d7fdaaa2c5921ea1b");
            Assert.AreEqual(CryptographyTasks.CalculateSHA256Hash("SHA-256 HASHED 2"), "5a823380219911aca9596f9f50f366f10348bb054b082c13bc89dce8b8301ef5");
            Assert.AreEqual(CryptographyTasks.CalculateSHA256Hash("SHA-256 HASHED 3"), "1c7affbe79277cec2e9a47e0f29e673f7903f97539e68afd16a8e30c5bd0f856");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComputeSHA256NullArgument()
        {
            CryptographyTasks.CalculateSHA256Hash(null);
        }
    }
}
