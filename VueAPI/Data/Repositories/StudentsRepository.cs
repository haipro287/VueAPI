using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository: Repository<Student>
    {
        public StudentsRepository(SchoolContext context) :base(context)
        {
        }
    }
}
