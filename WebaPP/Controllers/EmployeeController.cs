using BAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebaPP.Repository.Interface;

namespace WebaPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _iemprep;
        public EmployeeController(IEmployeeRepo iemprep) {
            _iemprep = iemprep;
        }
        [HttpPost]
        [Route("EmployeeRegistration")]
        public async Task<IActionResult> EmployeeRegistration(Employee employee)
        {
            return Ok(await _iemprep.EmployeeRegistration(employee));
        }
        [HttpPost]
        [Route("GetEmployeeDetails/{employeeid}")]
        public async Task<IActionResult> GetEmployeeDetails(string employeeid)
        {
            return Ok(await _iemprep.GetEmployeeDetailsList(employeeid));
        }
    }
}
