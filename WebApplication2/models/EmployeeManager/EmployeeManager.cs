using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Helpers;
using WebApplication2.models.Repository;

namespace WebApplication2.models.EmployeeManager
{
    public class EmployeeManager : IDataRepository <Employee>
    {
        readonly EmployeeContext _employeeContext;

       //  users hardcoded for simplicity, store in a db with hashed passwords in production applications
       // private List<Employee> _employees = new List<Employee>
       // {
       //     new Employee { Id = 1, Firstname = "Test", Lastname = "User", Username = "test",Email ="Test", Password = "test" }
       // };

       // private readonly AppSettings _appSettings;

       /* public EmployeeManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }*/

        public Employee Authenticate(Employee emp)
        {
             //var employee = _employees.SingleOrDefault(x => x.Username == username && x.Password == password);
              var employee = _employeeContext.Employees
             .Where(c => c.Username == emp.Username && c.Password == emp.Password)
             .Select(c => c.Username)
              .FirstOrDefault();
              
            // return null if user not found
            if (employee == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, emp.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            emp.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            emp.Password = null;

            return emp;
        } 

        
        //
        public EmployeeManager (EmployeeContext context)
        {
            _employeeContext = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _employeeContext.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return _employeeContext.Employees
                  .FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee entity)
        {
            _employeeContext.Employees.Add(entity);
            _employeeContext.SaveChanges();
        }

      
    }
}
