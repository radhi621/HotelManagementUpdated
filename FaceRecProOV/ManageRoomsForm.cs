using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class ManageRoomsForm : Form
    {
        public ManageRoomsForm()
        {
            InitializeComponent();
        }

        Room room = new Room();
        private void ManageRoomsForm_Load(object sender, EventArgs e)
        {
            cbRoomType.DataSource = room.RoomTypeList();
            cbRoomType.DisplayMember = "label";
            cbRoomType.ValueMember = "id";

            cbcard.DataSource = room.GetAvailableCards();
            cbcard.DisplayMember = "CardID";
            cbcard.ValueMember = "CardID";

            dgvRooms.DataSource = room.GetAllRooms();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
            string phone = tbPhone.Text;
            string free = "";
            int cardId = Convert.ToInt32(cbcard.SelectedValue.ToString());

            try
            {
                if (rbYes.Checked)
                {
                    free = "YES";
                }
                else if (rbNo.Checked)
                {
                    free = "NO";
                }

                if (room.InsertRoom(0, type, phone, free, cardId))
                {
                    dgvRooms.DataSource = room.GetAllRooms();
                    cbcard.DataSource = room.GetAvailableCards(); // Refresh available cards
                    MessageBox.Show("Room and card assigned successfully!", "Room Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Room not inserted!", "Room Add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
            string phone = tbPhone.Text;
            string free = "";

            try
            {
                int number = Convert.ToInt32(tbNumber.Text);  // This is the room number, not the ID

                if (rbYes.Checked)
                {
                    free = "YES";
                }
                else if (rbNo.Checked)
                {
                    free = "NO";
                }

                if (room.EditRoom(number, type, phone, free))  // Here, use tbNumber (room number) to identify the room
                {
                    dgvRooms.DataSource = room.GetAllRooms();
                    MessageBox.Show("Room data updated successfully!", "Room Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Room data not updated!", "Room Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(tbNumber.Text);  // This is the room number

                if (room.RemoveRoom(number))
                {
                    dgvRooms.DataSource = room.GetAllRooms();
                    MessageBox.Show("Room deleted successfully!", "Room Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Room not deleted!", "Room Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClearFields_Click(object sender, EventArgs e)
        {
            tbNumber.Text = "";
            cbRoomType.SelectedIndex = 0;
            tbPhone.Text = "";
            rbYes.Checked = true;
        }

     
        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbNumber.Text = dgvRooms.CurrentRow.Cells[0].Value.ToString();
            cbRoomType.SelectedValue = dgvRooms.CurrentRow.Cells[1].Value;
            tbPhone.Text = dgvRooms.CurrentRow.Cells[2].Value.ToString();

            string free  = dgvRooms.CurrentRow.Cells[3].Value.ToString();

            if (free.Equals("YES"))
            {
                rbYes.Checked = true;
                //rbNo.Checked = false;
            }
            else if (free.Equals("NO"))
            {
               // rbYes.Checked = false;
                rbNo.Checked = true;
            }


        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbcard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
