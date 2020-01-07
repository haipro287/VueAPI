using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Data.Services;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly EnrollmentsService EnrollmentsService;
        private readonly StudentsService StudentsService;
        private readonly CoursesService CoursesService;

        public EnrollmentsController(EnrollmentsService EnrollmentsService, StudentsService StudentsService, CoursesService CoursesService)
        {
            this.EnrollmentsService = EnrollmentsService;
            this.StudentsService = StudentsService;
            this.CoursesService = CoursesService;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await EnrollmentsService.GetAll().ToListAsync();
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await EnrollmentsService.Detail(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        // POST: api/Enrollments/
        [HttpPost]
        public async Task<IActionResult> PostEnrollment(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                await EnrollmentsService.Insert(enrollment);

                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentID }, enrollment);
            }
            return BadRequest();
        }

        // PUT: api/Enrollments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await EnrollmentsService.Update(enrollment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetEnrollments));
            }
            return BadRequest();
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await EnrollmentsService.Delete(id);
            return RedirectToAction(nameof(GetEnrollments));
        }

        private bool EnrollmentExists(int id)
        {
            return EnrollmentsService.GetAll().Any(e => e.EnrollmentID == id);
        }
    }
}
