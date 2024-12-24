using System;
using System.Data;
using System.Data.SqlClient;

namespace Hotel_System
{
    class Reservation
    {
        DBConnection conn = new DBConnection();

        // Get all reservations
        public DataTable GetAllReservations()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM reservations", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // Make a new reservation
        public bool MakeReservation(int number, int client, DateTime dateIn, DateTime dateOut)
        {
            string queryInsert = "INSERT INTO reservations (room_number, client_id, date_in, date_out) VALUES (@number, @client, @dateIn, @dateOut)";

            using (SqlCommand command = new SqlCommand(queryInsert, conn.GetConnection()))
            {
                command.Parameters.Add("@number", SqlDbType.Int).Value = number;
                command.Parameters.Add("@client", SqlDbType.Int).Value = client;
                command.Parameters.Add("@dateIn", SqlDbType.Date).Value = dateIn;
                command.Parameters.Add("@dateOut", SqlDbType.Date).Value = dateOut;

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

        // Edit reservation
        public bool EditReservation(int id, int number, int client, DateTime dateIn, DateTime dateOut)
        {
            string queryUpdate = "UPDATE reservations SET room_number=@number, client_id=@client, date_in=@dateIn, date_out=@dateOut WHERE id=@id";

            using (SqlCommand command = new SqlCommand(queryUpdate, conn.GetConnection()))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@number", SqlDbType.Int).Value = number;
                command.Parameters.Add("@client", SqlDbType.Int).Value = client;
                command.Parameters.Add("@dateIn", SqlDbType.Date).Value = dateIn;
                command.Parameters.Add("@dateOut", SqlDbType.Date).Value = dateOut;

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

        // Remove reservation
        public bool RemoveReservation(int id)
        {
            string queryDelete = "DELETE FROM reservations WHERE id=@id";

            using (SqlCommand command = new SqlCommand(queryDelete, conn.GetConnection()))
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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

        // Get the room type based on room ID
        public int GetRoomType(int roomId)
        {
            SqlCommand command = new SqlCommand("SELECT room_type_id FROM rooms WHERE room_number = @roomId", conn.GetConnection());
            command.Parameters.Add("@roomId", SqlDbType.Int).Value = roomId;
            conn.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();
            int roomTypeId = -1;

            if (reader.Read())
            {
                roomTypeId = Convert.ToInt32(reader["room_type_id"]);
            }

            conn.CloseConnection();
            return roomTypeId;
        }
    }
}
