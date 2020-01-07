using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data.Repositories
{
    public class DepartmentsRepository: Repository<Department>
    {
        public DepartmentsRepository(SchoolContext context): base(context)
        {
        }
    }
}
