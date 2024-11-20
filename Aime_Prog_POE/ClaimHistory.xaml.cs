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
    public partial class ClaimHistory : Window
    {
        public ClaimHistory()
        {
            InitializeComponent();
            LoadClaimHistory();  // Load claim history when the window is initialized
        }

        private void LoadClaimHistory()
        {
            string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";
            List<Claim> claims = new List<Claim>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ClaimId, LecturerName, HoursWorked, HourlyRate, AdditionalNotes, Status FROM LecturerClaims WHERE LecturerName = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Global.UserName); // Use Global.UserName here

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                claims.Add(new Claim
                                {
                                    ClaimId = reader.GetInt32(0),
                                    LecturerName = reader.GetString(1),
                                    HoursWorked = reader.GetInt32(2),
                                    HourlyRate = reader.GetDecimal(3),
                                    AdditionalNotes = reader.GetString(4),
                                    Status = reader.GetString(5)
                                });
                            }
                        }
                    }

                    // Bind the claim history data to a ListView or other UI element
                    lvClaimHistory.ItemsSource = claims;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading claims: " + ex.Message);
                }
            }
        }
    }
}
