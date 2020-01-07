using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data.Repositories
{
    public class EnrollmentsRepository : Repository<Enrollment>
    {
        public EnrollmentsRepository(SchoolContext context) : base(context)
        {
        }
    }
}
