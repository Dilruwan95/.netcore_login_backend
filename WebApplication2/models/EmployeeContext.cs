using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext (DbContextOptions <EmployeeContext> options)
            :base(options)
        {

        }

        public DbSet <Employee> Employees { get; set; }

        internal object SingleOrDefault(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
