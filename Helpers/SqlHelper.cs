﻿using System.Data;
using System.Data.SqlClient;

namespace Business.Helpers
{
    public static class SqlHelper
    {
        private const string _connectionStr = "Server=WIN-D0ANKQEOS98\\SQLEXPRESS;Database=Business;Trusted_Connection=true";

        public static async Task<DataTable> SelectAsync(string query)
        {
            using SqlConnection conn = new SqlConnection(_connectionStr);
            await conn.OpenAsync();
            using (SqlDataAdapter sda = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
        public static async Task<int> ExecuteAsync(string command)
        {

            using (SqlConnection conn = new SqlConnection(_connectionStr))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
