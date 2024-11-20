using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace Aime_Prog_POE
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }  // Change this to decimal
        public string AdditionalNotes { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }



    public partial class ClaimApproval : Window
        {
            public ClaimApproval()
            {
                InitializeComponent();
                LoadClaims();
            }

        private void LoadClaims()
        {
            string connectionString = "insert own connection string";
            List<Claim> claims = new List<Claim>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ClaimId, LecturerName, HoursWorked, HourlyRate, AdditionalNotes, Status FROM LecturerClaims";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                claims.Add(new Claim
                                {
                                    ClaimId = reader.GetInt32(0),  // Mapping to ClaimId instead of Id
                                    LecturerName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    HoursWorked = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    HourlyRate = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),  // Use GetDecimal instead of GetInt32
                                    AdditionalNotes = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                    Status = reader.IsDBNull(5) ? "Pending" : reader.GetString(5)
                                });
                            }
                        }
                    }

                    lvClaims.ItemsSource = claims; // Bind claims to the ListView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading claims: " + ex.Message);
                }
            }
        }

        private void lvClaims_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            {
                if (lvClaims.SelectedItem is Claim selectedClaim)
                {
                    txtClaimId.Text = selectedClaim.ClaimId.ToString();
                    txtLecturerName.Text = selectedClaim.LecturerName;
                    txtHoursWorked.Text = selectedClaim.HoursWorked.ToString("F2");
                    txtHourlyRate.Text = selectedClaim.HourlyRate.ToString("F2");
                    txtAdditionalNotes.Text = selectedClaim.AdditionalNotes;

                    btnApprove.IsEnabled = true;  // Enable Approve button
                    btnReject.IsEnabled = true;   // Enable Reject button
                }
                else
                {
                    btnApprove.IsEnabled = false;
                    btnReject.IsEnabled = false;
                }
            }

            private void ApproveClaim_Click(object sender, RoutedEventArgs e)
            {
                if (lvClaims.SelectedItem is Claim selectedClaim)
                {
                    LogClaimApproval(selectedClaim.ClaimId, true);  // Pass ClaimId to LogClaimApproval
                }
            }

            private void RejectClaim_Click(object sender, RoutedEventArgs e)
            {
                if (lvClaims.SelectedItem is Claim selectedClaim)
                {
                    LogClaimApproval(selectedClaim.ClaimId, false);  // Pass ClaimId to LogClaimApproval
                }
            }

            private void LogClaimApproval(int claimId, bool isApproved)
            {
                string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        string query = "UPDATE LecturerClaims SET Status = @Status WHERE ClaimId = @ClaimId";  // Use ClaimId instead of Id
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ClaimId", claimId);
                            cmd.Parameters.AddWithValue("@Status", isApproved ? "Approved" : "Rejected");

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show(isApproved ? "Claim approved." : "Claim rejected.");
                        LoadClaims();  // Reload claims to refresh the ListView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating claim: " + ex.Message);
                    }
                }
            }
        }
    }
