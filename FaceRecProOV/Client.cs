using System;
using System.Data;
using System.Data.SqlClient;  // Change MySQL namespace to SQL Server

namespace Hotel_System
{
    /*
     * Class for insert/update/delete/get all clients
     */
    class Client
    {
        DBConnection conn = new DBConnection();

        // Insert new client
        public bool InsertClient(string fname, string lname, string phone, string country)
        {
            SqlCommand command = new SqlCommand();
            string queryInsert = "INSERT INTO clients(first_name, last_name, phone, country) VALUES (@fname, @lname, @phone, @country)";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@fname", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@country", SqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        // Get all clients
        public DataTable GetAllClients()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM clients", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // Edit client data
        public bool EditClient(int id, string fname, string lname, string phone, string country)
        {
            SqlCommand command = new SqlCommand();
            string queryUpdate = "UPDATE clients SET first_name=@fname, last_name=@lname, phone=@phone, country=@country WHERE id=@cid";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", SqlDbType.Int).Value = id;
            command.Parameters.Add("@fname", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@country", SqlDbType.VarChar).Value = country;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        // Remove client
        public bool RemoveClient(int id)
        {
            SqlCommand command = new SqlCommand();
            string queryDelete = "DELETE FROM clients WHERE id=@cid";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", SqlDbType.Int).Value = id;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }
    }
}
