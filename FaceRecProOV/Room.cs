using System;
using System.Data;
using System.Data.SqlClient;

namespace Hotel_System
{
    class Room
    {
        DBConnection conn = new DBConnection();

        // Get all roomTypes

        public DataTable GetAvailableCards()
        {
            SqlCommand command = new SqlCommand("SELECT CardID, CardStatus FROM Cards WHERE CardStatus = 'Available'", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool AssignCardToRoom(int cardId, int roomNumber)
        {
            SqlCommand command = new SqlCommand("UPDATE Cards SET CardStatus = 'Assigned', RoomID = @roomId WHERE CardID = @cardId", conn.GetConnection());
            command.Parameters.Add("@cardId", SqlDbType.Int).Value = cardId;
            command.Parameters.Add("@roomId", SqlDbType.Int).Value = roomNumber;

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






        public DataTable RoomTypeList()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM rooms_type", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // Get all rooms based on type
        public DataTable RoomByType(int type)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM rooms WHERE type=@type and free = 'YES'", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@type", SqlDbType.Int).Value = type;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // Get room type id
        public int GetRoomType(int number)
        {
            SqlCommand command = new SqlCommand("SELECT type FROM rooms WHERE number=@number", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@number", SqlDbType.Int).Value = number;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return Convert.ToInt32(table.Rows[0][0].ToString());
        }

        // Set free to NO/YES
        public bool SetRoomFree(int number, string isFree)
        {
            SqlCommand command = new SqlCommand("UPDATE rooms SET free=@isFree WHERE number=@number", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            command.Parameters.Add("@number", SqlDbType.Int).Value = number;
            command.Parameters.Add("@isFree", SqlDbType.VarChar).Value = isFree;

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

        // Insert new room
        public bool InsertRoom(int number, int type, string phone, string free, int cardId)
        {
            SqlCommand command = new SqlCommand();
            string queryInsert = "INSERT INTO rooms (type, phone, free) VALUES (@type, @phone, @free); SELECT SCOPE_IDENTITY();";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", SqlDbType.VarChar).Value = free;

            conn.OpenConnection();
            int newRoomId = Convert.ToInt32(command.ExecuteScalar());

            if (newRoomId > 0)
            {
                // Update the room with the CardID after inserting the room
                SqlCommand updateRoomCommand = new SqlCommand("UPDATE rooms SET CardID = @cardId WHERE number = @roomId", conn.GetConnection());
                updateRoomCommand.Parameters.Add("@cardId", SqlDbType.Int).Value = cardId;
                updateRoomCommand.Parameters.Add("@roomId", SqlDbType.Int).Value = newRoomId;

                int rowsAffected = updateRoomCommand.ExecuteNonQuery(); // Ensure the room is updated with the CardID

                if (rowsAffected > 0)
                {
                    // Also update the Card status to 'Assigned' and link it to the room
                    AssignCardToRoom(cardId, newRoomId);
                    conn.CloseConnection();
                    return true;
                }
                else
                {
                    conn.CloseConnection();
                    return false;
                }
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }



        // Get all rooms
        public DataTable GetAllRooms()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM rooms", conn.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // Edit room data
        public bool EditRoom(int number, int type, string phone, string free)
        {
            SqlCommand command = new SqlCommand();
            string queryUpdate = "UPDATE rooms SET type=@type, phone=@phone, free=@free WHERE number=@number";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", SqlDbType.Int).Value = number;
            command.Parameters.Add("@type", SqlDbType.Int).Value = type;
            command.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", SqlDbType.VarChar).Value = free;

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

        // Remove room
        public bool RemoveRoom(int number)
        {
            SqlCommand command = new SqlCommand();
            string queryDelete = "DELETE FROM rooms WHERE number=@number";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", SqlDbType.Int).Value = number;

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
