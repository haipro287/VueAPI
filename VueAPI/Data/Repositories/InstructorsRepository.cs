using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data.Repositories
{
    public class InstructorsRepository: Repository<Instructor>
    {
        public InstructorsRepository(SchoolContext context): base(context)
        {
        }
    }
}
