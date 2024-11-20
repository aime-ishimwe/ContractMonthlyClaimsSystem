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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Debugging message to check the username and password
            Console.WriteLine($"Attempting login with Username: {username}, Password: {password}");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string connectionString = "Data Source=AIME\\SQLEXPRESS;Initial Catalog=Monthly_Claims_System;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        object result = cmd.ExecuteScalar();
                        string role = result as string; // Safely cast to string

                        if (role != null)
                        {
                            // Store user info globally
                            Global.UserName = username;
                            Global.Role = role;

                            MessageBox.Show($"Welcome, {role}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Pass username and role to the respective window
                            if (role == "Lecturer")
                            {
                                LecturerDashboard lecturerDashboard = new LecturerDashboard();
                                lecturerDashboard.Show();
                            }
                            else if (role == "HR")
                            {
                                HRDashboard hrDashboardWindow = new HRDashboard();
                                hrDashboardWindow.Show();
                            }
                            else if (role == "Coordinator")
                            {
                                ClaimApproval claimApprovalWindow = new ClaimApproval();
                                claimApprovalWindow.Show();
                            }

                            this.Close(); // Close the login window
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }
    }
}