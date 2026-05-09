using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EMS
{
    public static class DBHelper
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString;

        // =========================
        // GET CONNECTION
        // =========================
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // =========================
        // EXECUTE NON QUERY
        // =========================
        public static int ExecuteNonQuery(string query, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = type;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // GET DATA TABLE
        // =========================
        public static DataTable GetDataTable(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // =========================
        // EXECUTE SCALAR (optional but useful)
        // =========================
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}