using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EMS
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // =========================
            // GLOBAL ERROR HANDLING
            // =========================
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show(
                    "Unexpected Error:\n" + args.Exception.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show(
                    "Critical Error:\n" + args.ExceptionObject.ToString(),
                    "Fatal Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            };

            // =========================
            // DATABASE CONNECTION CHECK
            // =========================
            try
            {
                using (SqlConnection con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString))
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database Connection Failed:\n" + ex.Message,
                    "Startup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // =========================
            // START APPLICATION
            // =========================
            try
            {
                Application.Run(new MainDashboardForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Application failed to start:\n" + ex.Message,
                    "Startup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}