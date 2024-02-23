using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using static DAL.SqlDataHelper;

namespace DAL
{
    public class SqlDataHelper: IsqlDataHelper
    {  

            private readonly string _connectionString;
            private string exFolder = Path.Combine("ExceptionLogs");
            private string exPathToSave = string.Empty;
            public SqlDataHelper(IConfiguration configuration)
            {
                exPathToSave = Path.Combine(Directory.GetCurrentDirectory(), exFolder);
                _connectionString = configuration.GetConnectionString("Ezcompcon");
            }
        
            public async Task<int> ExcuteNonQueryasync(SqlCommand cmd)
            {
                SqlConnection sqlcon = new SqlConnection(_connectionString);
                int i = 0;
                try
                {
                  
                    i = await cmd.ExecuteNonQueryAsync();
                    await sqlcon.CloseAsync();
                    cmd.Dispose();
                    return i;

                }
                catch (Exception ex)
                {
                    sqlcon.Close();
                    cmd.Dispose();
                    throw ex;
                }

            }
            public async Task<DataTable> SqlDataAdapterasync(SqlCommand cmd)
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                SqlConnection sqlcon = new SqlConnection(_connectionString);
                DataTable dt = new DataTable();
                try
                {
                    await sqlcon.OpenAsync();
                    adp = new SqlDataAdapter(cmd);
                    await Task.Run(() => adp.Fill(dt));
                    await sqlcon.CloseAsync();
                    cmd.Dispose();
                    adp.Dispose();
                    return dt;
                }
                catch (Exception ex)
                {
                    sqlcon.Close();
                    cmd.Dispose();
                    adp.Dispose();
                    throw ex;
                }

            }
        }    
}