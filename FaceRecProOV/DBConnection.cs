using System;
using System.Data;
using System.Data.SqlClient; // SQL Server client
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_System
{
    class DBConnection
    {
        // Connection string for SQL Server (adjust as needed)
        private SqlConnection _connection = new SqlConnection("Server=localhost;Database=hotel_system;Integrated Security=True;");

        // Return connection
        public SqlConnection GetConnection()
        {
            return _connection;
        }

        // Open connection
        public void OpenConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch (SqlException ex)
            {
                // Handle any errors that occur during connection
                Console.WriteLine("Error opening connection: " + ex.Message);
            }
        }

        // Close connection
        public void CloseConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (SqlException ex)
            {
                // Handle any errors that occur during closing
                Console.WriteLine("Error closing connection: " + ex.Message);
            }
        }
    }
}
