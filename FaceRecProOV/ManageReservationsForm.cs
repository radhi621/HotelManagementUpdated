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
    public partial class ManageReservationsForm : Form
    {
        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        Room room = new Room();
        Reservation reservation = new Reservation();

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            // Display room types
            cbRoomType.DataSource = room.RoomTypeList();
            cbRoomType.DisplayMember = "label";
            cbRoomType.ValueMember = "id";

            // Display free room numbers depending on selected type
            int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
            cbRoomNumber.DataSource = room.RoomByType(type);
            cbRoomNumber.DisplayMember = "number";
            cbRoomNumber.ValueMember = "number";

            // Load all reservations
            dgvReservations.DataSource = reservation.GetAllReservations();
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            tbReservID.Text = "";
            tbClientID.Text = "";
            cbRoomType.SelectedIndex = 0;
            dateTimePickerIN.Value = DateTime.Now;
            dateTimePickerOUT.Value = DateTime.Now;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int clientID = Convert.ToInt32(tbClientID.Text);
                int roomNumber = Convert.ToInt32(cbRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The check-in date must be greater than or equal to the current date.", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut <= dateIn)
                {
                    MessageBox.Show("The check-out date must be later than the check-in date.", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.MakeReservation(roomNumber, clientID, dateIn, dateOut))
                    {
                        room.SetRoomFree(roomNumber, "NO");
                        dgvReservations.DataSource = reservation.GetAllReservations();
                        MessageBox.Show("Reservation made successfully!", "Make Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClearFields.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Reservation not added!", "Make Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Make Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int reservationID = Convert.ToInt32(tbReservID.Text);
                int clientID = Convert.ToInt32(tbClientID.Text);
                int roomNumber = Convert.ToInt32(dgvReservations.CurrentRow.Cells["room_number"].Value.ToString());
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The check-in date must be greater than or equal to the current date.", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut <= dateIn)
                {
                    MessageBox.Show("The check-out date must be later than the check-in date.", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.EditReservation(reservationID, roomNumber, clientID, dateIn, dateOut))
                    {
                        room.SetRoomFree(roomNumber, "NO");
                        dgvReservations.DataSource = reservation.GetAllReservations();
                        MessageBox.Show("Reservation data updated!", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Reservation not updated!", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int reservationID = Convert.ToInt32(tbReservID.Text);
                int roomNumber = Convert.ToInt32(dgvReservations.CurrentRow.Cells["room_number"].Value.ToString());

                if (reservation.RemoveReservation(reservationID))
                {
                    room.SetRoomFree(roomNumber, "YES");
                    dgvReservations.DataSource = reservation.GetAllReservations();
                    MessageBox.Show("Reservation deleted successfully!", "Reservation Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Reservation not deleted!", "Reservation Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
                cbRoomNumber.DataSource = room.RoomByType(type);
                cbRoomNumber.DisplayMember = "number";
                cbRoomNumber.ValueMember = "number";
            }
            catch (Exception ex)
            {
                // You can display an error message here if needed
            }
        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbReservID.Text = dgvReservations.CurrentRow.Cells["id"].Value.ToString();
            int roomID = Convert.ToInt32(dgvReservations.CurrentRow.Cells["room_number"].Value.ToString());

            // Set room type based on room ID
            cbRoomType.SelectedValue = room.GetRoomType(roomID);

            // Set room number based on room ID
            cbRoomNumber.SelectedValue = roomID;

            tbClientID.Text = dgvReservations.CurrentRow.Cells["client_id"].Value.ToString();
            dateTimePickerIN.Value = Convert.ToDateTime(dgvReservations.CurrentRow.Cells["date_in"].Value.ToString());
            dateTimePickerOUT.Value = Convert.ToDateTime(dgvReservations.CurrentRow.Cells["date_out"].Value.ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
