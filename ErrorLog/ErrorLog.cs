using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http; // Add this namespace for IHttpContextAccessor
using System;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;

namespace ErrorLog
{
    public class ErrorLogger
    {
        public static string connectionString = "Server=217.76.53.78;Database=ConvergeDB;User Id=ConvergeUsr;Password=C0nv3rg3;Trusted_Connection=True;Integrated Security=False;TrustServerCertificate=True";

        public static async Task LogError(string file,string methodName,string request, Exception ex)
        {
            try
            {
               
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();  // Open connection to the database
                    using (SqlCommand cmd = new SqlCommand("SaveErrorLog", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters dynamically based on provided values
                        cmd.Parameters.AddWithValue("@File", file ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MethodName", methodName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Exception", ex?.Message ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@StackTrace", ex?.StackTrace ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Request", request);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                        await cmd.ExecuteNonQueryAsync();  // Execute the stored procedure
                    }
                }
            }
            catch (Exception e)
            {
                // Log the error message if logging to the database fails
                Console.WriteLine($"Error logging exception: {e.Message}");
            }
        }

        public static string LogMethodParameters(string methodName, Dictionary<string, object> parameters)
        {
            try
            {
                string logMessage = $"{methodName} called with parameters: ";

                foreach (var param in parameters)
                {
                    logMessage += $"{param.Key}={param.Value}, ";

                }
                logMessage = logMessage.TrimEnd(new char[] { ',', ' ' });
                return logMessage;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static Dictionary<string, object> ConvertObjectToDictionary(object obj)
        {
            var dictionary = new Dictionary<string, object>();

            // Get all properties of the object
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Get property name and value, and add to dictionary
                dictionary.Add(property.Name, property.GetValue(obj));
            }

            return dictionary;
        }

        public static Dictionary<string, object> ConvertToDictionary(params object[] args)
        {
            try
            {
                var dictionary = new Dictionary<string, object>();
                // Ensure the params are in key-value pairs
                if (args.Length % 2 != 0)
                {
                    throw new ArgumentException("Arguments must be in key-value pairs (name-value).");
                }

                for (int i = 0; i < args.Length; i += 2)
                {
                    if (args[i] is not string key)
                    {
                        throw new ArgumentException($"Argument at position {i} must be a string (key).");
                    }

                    dictionary[key] = args[i + 1]; // Add key-value pair to dictionary
                }

                return dictionary;
            }
            catch (Exception)
            {
                return new Dictionary<string, object> { { "Error", "In the method ConvertToDictionary" } };
            }
        }
    }
}
