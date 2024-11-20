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

namespace Aime_Prog_POE
{
    public partial class LecturerDashboard : Window
    {
        public LecturerDashboard()
        {
            InitializeComponent();
            lblLecturerName.Content = "Welcome, " + Global.UserName;  // Display logged-in user's name
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            // Open Claim Submission window
            new ClaimSubmission().Show();
        }

        private void ViewClaimHistory_Click(object sender, RoutedEventArgs e)
        {
            // Open Claim History window
            new ClaimHistory().Show();
        }
    }
}