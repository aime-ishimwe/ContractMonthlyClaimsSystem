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
using System.Data;

namespace Aime_Prog_POE
{
    public partial class HRDashboard : Window
    {
        private string connectionString = "insert own connection string";

        public HRDashboard()
        {
            InitializeComponent();
        }

        // Generate and display the report of approved claims
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // Fetch approved claims data and calculate total for invoice
            GenerateInvoice();
        }

        // Method to load approved claims and generate an invoice report
        private void GenerateInvoice()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch approved claims data with total calculation
                    string query = "SELECT LecturerName, HoursWorked, HourlyRate, (HoursWorked * HourlyRate) AS TotalClaim, AdditionalNotes, Status FROM LecturerClaims WHERE Status = 'Approved'";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the DataGrid to display the invoice
                    dgClaims.ItemsSource = dt.DefaultView;

                    // Optionally, save the invoice to an external file (e.g., Excel or CSV)
                    // SaveInvoiceToFile(dt);

                    MessageBox.Show("Invoice generated for approved claims.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating the invoice: " + ex.Message);
            }
        }

        // Method to save invoice data to a file (optional)
        private void SaveInvoiceToFile(DataTable dataTable)
        {
            // Implement logic to save data to a file, e.g., CSV, or Excel
            /*
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = (Worksheet)workbook.Sheets[1];

            // Write column headers
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            // Write data
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                }
            }

            // Optionally save the invoice
            workbook.SaveAs("C:\\path\\to\\save\\ApprovedClaimsInvoice.xlsx");
            */
        }

        // Manage lecturer data (e.g., update personal/contact details)
        private void ManageLecturerData_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have a window for managing lecturer data
            string lecturerId = "someLecturerId"; // Replace with actual logic to get the lecturer ID
            ManageLecturerDataWindow manageWindow = new ManageLecturerDataWindow(lecturerId);
            manageWindow.Show();
        }
    }
}
