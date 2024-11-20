using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Aime_Prog_POE
{
    public partial class ManageLecturerDataWindow : Window
    {
        private string lecturerId;

        // Connection string to your database
        private string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";

        // Constructor that accepts the lecturerId to load their data
        public ManageLecturerDataWindow(string lecturerId)
        {
            InitializeComponent();
            this.lecturerId = lecturerId;

            // Load lecturer data based on the lecturerId
            LoadLecturerData();
        }

        // Method to load the lecturer's data from the database
        private void LoadLecturerData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to fetch lecturer details
                    string query = "SELECT LecturerName, LecturerEmail, LecturerPhone FROM Lecturers WHERE LecturerId = @LecturerId";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate the UI with lecturer data
                        LecturerNameTextBox.Text = reader["LecturerName"].ToString();
                        LecturerEmailTextBox.Text = reader["LecturerEmail"].ToString();
                        LecturerPhoneTextBox.Text = reader["LecturerPhone"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Lecturer not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lecturer data: " + ex.Message);
            }
        }

        // Method to update the lecturer's data in the database
        private void UpdateLecturerData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to update the lecturer details
                    string query = "UPDATE Lecturers SET LecturerName = @LecturerName, LecturerEmail = @LecturerEmail, LecturerPhone = @LecturerPhone WHERE LecturerId = @LecturerId";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@LecturerId", lecturerId);
                    cmd.Parameters.AddWithValue("@LecturerName", LecturerNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@LecturerEmail", LecturerEmailTextBox.Text);
                    cmd.Parameters.AddWithValue("@LecturerPhone", LecturerPhoneTextBox.Text);

                    // Execute the update command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lecturer data updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No data was updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating lecturer data: " + ex.Message);
            }
        }
    }
}
