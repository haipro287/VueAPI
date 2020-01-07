using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Services
{
    public class StudentsService
    {
        private readonly StudentsRepository StudentsRepository;
        private readonly EnrollmentsRepository EnrollmentsRepository;

        public StudentsService(StudentsRepository StudentsRepository, EnrollmentsRepository EnrollmentsRepository)
        {
            this.StudentsRepository = StudentsRepository;
            this.EnrollmentsRepository = EnrollmentsRepository;
        }

        public DbSet<Student> GetAll()
        {
            return StudentsRepository.GetAll();
        }

        public async Task<Student> GetByID(object id)
        {
            return await StudentsRepository.GetByID(id);
        }

        public async Task Insert(Student s)
        {
            await StudentsRepository.Insert(s);
        }
        
        public async Task Update(Student s)
        {
            await StudentsRepository.Update(s);
        }

        public async Task Delete(object id)
        {
            await StudentsRepository.Delete(id);
            foreach (Enrollment e in EnrollmentsRepository.GetAll().Where(e => e.StudentID == (int)id)) {
                await EnrollmentsRepository.Delete(e);
            }
        }

        public async Task<Student> FindOrDefault(int? id)
        {
            return await StudentsRepository.GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public IQueryable<Student> Index(string searchString, string sortOrder)
        {
            var students = from s in StudentsRepository.GetAll()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            return students;
        }

        public async Task<Student> Detail(int? id)
        {
            return await StudentsRepository.GetAll()
                    .Include(s => s.Enrollments)
                        .ThenInclude(e => e.Course)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);
        }
    }
}
