using BAL.Models;

namespace WebaPP.Repository.Interface
{
    public interface IEmployeeRepo
    {
        Task<Response> EmployeeRegistration(Employee employee);
        Task<EmplopyeeDetailsResponse> GetEmployeeDetailsList(string employeeId);
    }
}
