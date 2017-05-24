namespace WorkProjectTasks
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public static class ADONetTasks
    {
        /// <summary>
        /// Returns the TaskDB connection string.
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TaskDBConnectionString"].ConnectionString;
            }
        }

        /// <summary>
        /// Creates a new user and returns the unique user id.
        /// </summary>
        /// <param name="emailAddress">The user's email address.</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <returns>A unique 32 bit signed integer that represents the newly created user.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="emailAddress"/>, <paramref name="firstName"/> or <paramref name="lastName"/> is null.</exception>
        public static int CreateUser(string emailAddress, string firstName, string lastName)
        {
            // Test input parameters
            if (emailAddress == null || firstName == null || lastName == null)
            {
                throw new ArgumentNullException("The emailAddress, firstName or lastName is null.", new Exception());
            }

            // Should also, but was not asked to, test if emailAddress already exists before adding
            if (UserWithEmailAddressExists(emailAddress))
            {
                throw new ArgumentException("The emailAddress is already in registered.", "emailAddress");
            }

            // Declare results and sql transaction for rollback
            int results = 0;
            SqlTransaction trans = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    // Write the SQL
                    string commandString = @" 
                        INSERT INTO [dbo].[Users]
                        (
                            [EmailAddress]
                           ,[FirstName]
                           ,[LastName]
                        )
                        VALUES
                        (
                            @EmailAddress
                           ,@FirstName
                           ,@LastName
                        );

                        SELECT SCOPE_IDENTITY()";

                    // Open the connection and start the transaction
                    conn.Open();
                    trans = conn.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(commandString, conn, trans))
                    {
                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@EmailAddress", emailAddress));
                        command.Parameters.Add(new SqlParameter("@FirstName", firstName));
                        command.Parameters.Add(new SqlParameter("@LastName", lastName));

                        // Execute and read result
                        int.TryParse(command.ExecuteScalar().ToString(), out results);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }

                    // Re-throw
                    throw ex;
                }
            }

            return results;
        }

        /// <summary>
        /// Determines if a user exists with the specified email address.
        /// </summary>
        /// <param name="emailAddress">The user's email address.</param>
        /// <returns>true if the specified email address exists in the database; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="emailAddress"/> is null.</exception>
        public static bool UserWithEmailAddressExists(string emailAddress)
        {
            // Test input parameters
            if (emailAddress == null)
            {
                throw new ArgumentNullException("The emailAddress is null.", new Exception());
            }

            // Declare results and sql transaction for rollback
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // Write the SQL
                string commandString = @" 
                    SELECT 
                        (CASE WHEN COUNT(*) > 0 THEN 'true' ELSE 'false' END) as userCount
                    FROM
                        [dbo].[Users]
                    WHERE
                        [EmailAddress] = @EmailAddress";

                // Open the connection and start the transaction
                conn.Open();

                using (SqlCommand command = new SqlCommand(commandString, conn))
                {
                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@EmailAddress", emailAddress));

                    // Execute and read result
                    return bool.Parse(command.ExecuteScalar().ToString());
                }

            }
        }
    }
}
