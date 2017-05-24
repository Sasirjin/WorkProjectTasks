namespace WorkProjectTasks
{
   using System;
   using System.Security.Cryptography;
   using System.Text;

   public static class CryptographyTasks
   {
      /// <summary>
      /// Computes a SHA-256 hash from the specified data.
      /// </summary>
      /// <param name="data"></param>
      /// <returns>A SHA-256 hash of the specified <paramref name="data"/>, e.g. "SHA-256 HASHED" = "c88ba9717a37ae6110fdf313cac8356bc6ef29cd5cb9bb6d7fdaaa2c5921ea1b".</returns>
      /// <exception cref="ArgumentNullException">The data is null.</exception>
      /// <remarks>When computing hash, use UTF-8 encoding.</remarks>
      public static string CalculateSHA256Hash(string data) 
      {
         // Test input parameter
         if (data == null) 
         {
            throw new ArgumentNullException("data", "The data is null.");
         }

         // Initialize empty strings
         string resultString = string.Empty;
         SHA256Managed SHA256ManagedString = new SHA256Managed();

         // Transform into array
         byte[] dataBytesArray = Encoding.UTF8.GetBytes(data);
         byte[] hash = SHA256ManagedString.ComputeHash(dataBytesArray);

         // Write array to string
         foreach (byte dataByte in hash) 
         {
            resultString += String.Format("{0:x2}", dataByte);
         }

         return resultString;
      }
   }
}