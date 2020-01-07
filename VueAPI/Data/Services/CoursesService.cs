using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Services
{
    public class CoursesService
    {
        private readonly CoursesRepository CoursesRepository;
        private readonly EnrollmentsRepository EnrollmentsRepository;

        public CoursesService(CoursesRepository CoursesRepository, EnrollmentsRepository EnrollmentsRepository)
        {
            this.CoursesRepository = CoursesRepository;
            this.EnrollmentsRepository = EnrollmentsRepository;
        }

        public DbSet<Course> GetAll()
        {
            return CoursesRepository.GetAll();
        }

        public async Task<Course> GetByID(object id)
        {
            return await CoursesRepository.GetByID(id);
        }

        public async Task Insert(Course c)
        {
            await CoursesRepository.Insert(c);
        }

        public async Task Update(Course c)
        {
            await CoursesRepository.Update(c);

        }

        public async Task Delete(object id)
        {
            await CoursesRepository.Delete(id);
            foreach (Enrollment e in EnrollmentsRepository.GetAll().Where(e => e.CourseID == (int)id))
            {
                await EnrollmentsRepository.Delete(e);
            }
        }

        public async Task<Course> FindOrDefault(int? id)
        {
            return await CoursesRepository.GetAll().FirstOrDefaultAsync(m => m.CourseID == id);
        }

        public async Task<Course> Detail(int? id)
        {
            return await CoursesRepository.GetAll()
                    .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Student)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.CourseID == id);
        }
    }
}
