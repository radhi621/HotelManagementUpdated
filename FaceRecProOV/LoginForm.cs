using MultiFaceRec;
using System;
using System.Data;
using System.Data.SqlClient;  // Use SqlClient for SQL Server
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection conn = new DBConnection();
                DataTable table = new DataTable();

                // Use SQL Server Command and DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand();
                string query = "SELECT * FROM users WHERE username = @username AND password = @password";

                command.CommandText = query;
                command.Connection = conn.GetConnection();

                // Adding parameters for SQL Server
                command.Parameters.Add("@username", SqlDbType.VarChar).Value = tbUsername.Text;
                command.Parameters.Add("@password", SqlDbType.VarChar).Value = tbPassword.Text;

                // Fill the table with results from the query
                adapter.SelectCommand = command;
                adapter.Fill(table);

                // Check if the username and password exist
                if (table.Rows.Count > 0)
                {
                    // If credentials are correct, hide the login form and show the main form
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    // Show error messages based on input
                    if (tbUsername.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Enter your username to Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (tbPassword.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Enter your Password to Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("This username or password does not exist", "Wrong Username/Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Handle any exceptions
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void facebutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
        }
    }
}
