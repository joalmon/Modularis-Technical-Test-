using ModularisTest.Enums;
using ModularisTest.Interfaces;
using System;
using System.Configuration;

namespace ModularisTest 
{
    public class DatabaseLogStrategy : ILogStrategy
    {
        public void Log(string message, MessageType type)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            // IMPLEMENTATION NOTE: Database connectivity is mocked for this assessment.
            // using (SqlConnection connection = new SqlConnection(connectionString)) {
            //    string query = "INSERT INTO Log (Message, Type) VALUES (@msg, @type)";
            //    ... logic ...
            // }

            // For demonstration purposes, we redirect the output to the console
            Console.WriteLine($"[DATABASE] Simulating DB insert: {message} with Type: {type}");
        }
    }
}
