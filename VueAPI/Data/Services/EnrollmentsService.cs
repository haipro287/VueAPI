using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Services
{
    public class EnrollmentsService
    {
        private readonly EnrollmentsRepository EnrollmentsRepository;
        private readonly StudentsRepository StudentsRepository;
        private readonly CoursesRepository CoursesRepository;
        public EnrollmentsService(EnrollmentsRepository EnrollmentsRepository, StudentsRepository StudentsRepository, CoursesRepository CoursesRepository)
        {
            this.EnrollmentsRepository = EnrollmentsRepository;
            this.StudentsRepository = StudentsRepository;
            this.CoursesRepository = CoursesRepository;
        }

        public IQueryable<Enrollment> GetAll()
        {
            return EnrollmentsRepository.GetAll();
        }

        public async Task<Enrollment> Detail(int? id)
        {
            return await EnrollmentsRepository.GetAll()
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
        }

        public async Task Insert(Enrollment e)
        {
            await EnrollmentsRepository.Insert(e);
        }

        public async Task<Enrollment> Find(int? id)
        {
            return await EnrollmentsRepository.GetByID(id);
        }

        public async Task Update(Enrollment e)
        {
            await EnrollmentsRepository.Update(e);
        }

        public async Task Delete(int? id)
        {
            await EnrollmentsRepository.Delete(id);
        }
    }
}
