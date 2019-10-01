using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.models.EmployeeManager;
using WebApplication2.models.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly IDataRepository<Employee> _dataRepository;
     


        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }

  

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Employee emp)
        {
            var employee = _dataRepository.Authenticate(emp);

            if (employee == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(emp.Token);
        }

        // GET: api/Employee
        [HttpGet("getAll")]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public IActionResult GetAllEmployees()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }

        // POST api/<controller>
        [HttpPost("AddUser")]
        public IActionResult Post([FromBody] Employee employee)
        {
        
            if(employee == null)
            {
                return BadRequest("Employee is null.");
            }

                _dataRepository.Add(employee);
                return Ok(employee);
           

         
        }




       
    }
}
