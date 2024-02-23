using BAL.BusinessLogic.Interface;
using BAL.Common;
using BAL.Models;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.BusinessLogic.Helper
{
    public class EmplopyeeHelper: IEmplopyeeHelper
    {
        private readonly IsqlDataHelper _isqlDataHelper;
        private readonly string _connectionString;
        private string exFolder = Path.Combine("EmplopyeeExceptionLogs");
        private string exPathToSave = string.Empty;
        public EmplopyeeHelper(IConfiguration configuration,IsqlDataHelper isqlDataHelper) {
            _isqlDataHelper= isqlDataHelper;
            _connectionString = configuration.GetConnectionString("Ezcompcon");
            exPathToSave = Path.Combine(Directory.GetCurrentDirectory(), exFolder);
        }

        public async Task<string> SaveEmployeeData(Employee employee)
        {
            SqlConnection sqlcon = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd = new SqlCommand("SP_EmployeeRegData", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PemployeeCode", employee.employeeCode);
                cmd.Parameters.AddWithValue("@PemployeeName", employee.employeeName);
                cmd.Parameters.AddWithValue("@PfatherName", employee.fatherName);         
                cmd.Parameters.AddWithValue("@Pdepartment", employee.department);
                cmd.Parameters.AddWithValue("@Pdob", employee.dob);
                cmd.Parameters.AddWithValue("@Pdoj", employee.doj);
                cmd.Parameters.AddWithValue("@Pdol", employee.dol);
                cmd.Parameters.AddWithValue("@Page", employee.age);
                cmd.Parameters.AddWithValue("@PbankAccount", employee.bankAccount);
                cmd.Parameters.AddWithValue("@PbankName", employee.bankName);
                cmd.Parameters.AddWithValue("@Pgender", employee.gender);
                cmd.Parameters.AddWithValue("@Plocation", employee.location);
                cmd.Parameters.AddWithValue("@Pstate", employee.state);            
                await sqlcon.OpenAsync();
                await _isqlDataHelper.ExcuteNonQueryasync(cmd);
                return "Success";
            }
            catch (Exception ex)
            {
                Task WriteTask = Task.Factory.StartNew(() => LogFileException.Write_Log_Exception(exPathToSave, "GetPharmacyMaster_SP :  errormessage:" + ex.Message.ToString()));

                throw ex;
            }
        }

        public async Task<DataTable> GetEmployeeData(string EmpId)
        {
            SqlConnection sqlcon = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd = new SqlCommand("SP_EmployeeRegData", sqlcon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PempId", EmpId);
                return await Task.Run(() => _isqlDataHelper.SqlDataAdapterasync(cmd));
            }
            catch (Exception ex)
            {
                Task WriteTask = Task.Factory.StartNew(() => LogFileException.Write_Log_Exception(exPathToSave, "GetPharmacyMaster_SP :  errormessage:" + ex.Message.ToString()));

                throw ex;
            }
        }
    }
}
