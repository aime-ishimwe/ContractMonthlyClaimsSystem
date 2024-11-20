using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aime_Prog_POE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler to launch the login window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Show login window when user clicks the "Login" button
            Login loginWindow = new Login();
            loginWindow.ShowDialog();  // Use ShowDialog to ensure it's modal (user must log in)
        }

        // Event handler to submit claim for logged-in user
        private void LecturerSubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Global.UserName))  // Check if the user is logged in
            {
                MessageBox.Show("Please log in first.");
                return;
            }

            // Create a ClaimSubmission window for the logged-in user (no username parameter needed)
            ClaimSubmission claimSubmissionWindow = new ClaimSubmission();
            claimSubmissionWindow.Show();
        }

        // Event handler for Coordinator to approve claims
        private void CoordinatorApproveClaim_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Global.UserName))  // Check if the user is logged in
            {
                MessageBox.Show("Please log in first.");
                return;
            }

            // Launch the ClaimApproval window
            ClaimApproval claimApprovalWindow = new ClaimApproval();
            claimApprovalWindow.Show();
        }

        // Event handler for exit button
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Event handler for registration button (if needed)
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }
    }
}