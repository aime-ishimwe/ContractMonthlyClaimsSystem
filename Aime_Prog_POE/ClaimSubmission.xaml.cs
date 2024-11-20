using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Aime_Prog_POE
{
    public partial class ClaimSubmission : Window
    {
        private string filePath = string.Empty;

        public ClaimSubmission()
        {
            InitializeComponent();
            txtLecturerName.Text = Global.UserName;  // Automatically set LecturerName to the logged-in username
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";

            if (!ValidateInput())
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Retrieve the UserId of the logged-in user
            int userId = GetCurrentUserId();

            if (userId == -1)
            {
                MessageBox.Show("Invalid user or user not logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Insert the claim into the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = @"
                        INSERT INTO LecturerClaims (UserId, LecturerName, HoursWorked, HourlyRate, AdditionalNotes, Status, FilePath) 
                        VALUES (@UserId, @LecturerName, @HoursWorked, @HourlyRate, @AdditionalNotes, 'Pending', @FilePath);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@LecturerName", Global.UserName);
                        cmd.Parameters.AddWithValue("@HoursWorked", int.Parse(txtHoursWorked.Text));
                        cmd.Parameters.AddWithValue("@HourlyRate", decimal.Parse(txtHourlyRate.Text));
                        cmd.Parameters.AddWithValue("@AdditionalNotes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@FilePath", string.IsNullOrEmpty(filePath) ? DBNull.Value : filePath);

                        cmd.ExecuteNonQuery();
                    }

                    // Success message and clear fields
                    lblStatus.Content = "Claim submitted successfully. Status: Pending";
                    txtHoursWorked.Clear();
                    txtHourlyRate.Clear();
                    txtNotes.Clear();
                    filePath = string.Empty;
                    lblFileName.Content = "No file selected";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private int GetCurrentUserId()
        {
            string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT UserId FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Global.UserName);  // Use Global.UserName here
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching UserId: " + ex.Message);
                return -1;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtHoursWorked.Text) || !int.TryParse(txtHoursWorked.Text, out _))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtHourlyRate.Text) || !decimal.TryParse(txtHourlyRate.Text, out _))
            {
                return false;
            }

            return true;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                lblFileName.Content = "Selected file: " + Path.GetFileName(filePath);
            }
        }
    }
}