namespace WorkProjectTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Transactions;
    using WorkProjectTasks;

    [TestClass]
    public class ADONetTests
    {
        private const string USER_EMAILADDRESS = "user@email.com";
        private const string USER_FIRSTNAME = "John";
        private const string USER_LASTNAME = "Smith";

        [TestMethod]
        public void CreateTestUser()
        {
            Assert.IsTrue(TestCreateUserWithImplicitRollback(emailAddress: USER_EMAILADDRESS, firstName: USER_FIRSTNAME, lastName: USER_LASTNAME) >= 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTestUserWithNullEmailAddress()
        {
            TestCreateUserWithImplicitRollback(emailAddress: null, firstName: USER_FIRSTNAME, lastName: USER_LASTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTestUserWithNullFirstName()
        {
            TestCreateUserWithImplicitRollback(emailAddress: USER_EMAILADDRESS, firstName: null, lastName: USER_LASTNAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTestUserWithNullLastName()
        {
            TestCreateUserWithImplicitRollback(emailAddress: USER_EMAILADDRESS, firstName: USER_FIRSTNAME, lastName: null);
        }

        [TestMethod]
        public void UserExists()
        {
            Assert.IsTrue(ADONetTasks.UserWithEmailAddressExists("jane@doe.com"));
            Assert.IsFalse(ADONetTasks.UserWithEmailAddressExists("na"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserWithEmailAddressExistsNullArgument()
        {
            ADONetTasks.UserWithEmailAddressExists(null);
        }

        private int TestCreateUserWithImplicitRollback(string emailAddress, string firstName, string lastName)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout }))
                return ADONetTasks.CreateUser(emailAddress: emailAddress, firstName: firstName, lastName: lastName);
        }
    }
}
