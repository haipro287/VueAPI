using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data.Repositories
{
    public class CoursesRepository : Repository<Course>
    {
        public CoursesRepository(SchoolContext context) :base(context)
        {
        }
    }
}
