using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Models
{
    public class EmplopyeeDetailsResponse
    {
        public int statusCode {  get; set; }    
        public string message { get; set; }
        public List<Employee> employeeslist { get; set; }
    }
}
