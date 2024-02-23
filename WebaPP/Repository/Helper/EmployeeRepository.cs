using BAL.BusinessLogic.Interface;
using BAL.Common;
using BAL.Models;
using System.Collections.Generic;
using System.Data;
using WebaPP.Repository.Interface;

namespace WebaPP.Repository.Helper
{
    public class EmployeeRepository: IEmployeeRepo
    {
        private readonly IEmplopyeeHelper _iEmplopyeeHelper;
        public EmployeeRepository(IEmplopyeeHelper emplopyeeHelper) {
            _iEmplopyeeHelper=emplopyeeHelper;
        }

        public async Task<Response> EmployeeRegistration(Employee employee)
        {
            Response response = new Response();
            try
            {
                string status = await _iEmplopyeeHelper.SaveEmployeeData(employee);
                if (status.Equals("Success"))
                {
                    response.status = 100;
                    response.message = Constant.employeeRegistrationSuccessMsg;
                }
            }
            catch (Exception ex) { 
                response.status = 102;
                response.message = ex.Message;

            }
            return response;
        }

        public async Task<EmplopyeeDetailsResponse> GetEmployeeDetailsList(string employeeId)
        {
            EmplopyeeDetailsResponse response = new EmplopyeeDetailsResponse();
            try
            {
                DataTable dtresult= await _iEmplopyeeHelper.GetEmployeeData(employeeId);
                if (dtresult != null && dtresult.Rows.Count > 0)
                {
                    response.statusCode = 100;
                    response.message = Constant.employeeRegistrationSuccessMsg;
                    response.employeeslist = ConvertDataTabletoList(dtresult);
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 102;
                response.message = ex.Message;
                response.employeeslist = new List<Employee>();

            }
            return response;
        }

        private List<Employee> ConvertDataTabletoList(DataTable dt)
        {
            List<Employee> lstemployee = new List<Employee>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Employee employee = new Employee();

                    employee.employeeCode = row["employeeCode"].ToString();
                    employee.employeeName = row["employeeName"].ToString();
                    employee.fatherName = row["fatherName"].ToString();
                    employee.gender = row["gender"].ToString();
                    employee.age = Convert.ToInt32(row["age"].ToString());
                    employee.department = row["department"].ToString();
                    employee.doj = row["doj"].ToString();
                    employee.dob = row["dob"].ToString();
                    employee.dol = row["dol"].ToString();
                    employee.bankName = row["bankName"].ToString();
                    employee.bankAccount = row["bankAccount"].ToString();
                    employee.location = row["location"].ToString();
                    employee.state = row["state"].ToString();
                    lstemployee.Add(employee);

                }
                return lstemployee;
            }
            else
                return lstemployee;
        }

    }
}
