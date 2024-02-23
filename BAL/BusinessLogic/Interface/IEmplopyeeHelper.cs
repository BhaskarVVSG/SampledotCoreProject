using BAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.BusinessLogic.Interface
{
    public interface IEmplopyeeHelper
    {
        Task<string> SaveEmployeeData(Employee employee);
        Task<DataTable> GetEmployeeData(string EmpId);
    }
}
