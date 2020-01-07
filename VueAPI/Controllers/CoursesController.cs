using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Services;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesService CoursesService;

        public CoursesController(CoursesService CoursesService)
        {
            this.CoursesService = CoursesService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await CoursesService.GetAll().AsNoTracking().ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await CoursesService.Detail(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // POST: api/Courses/
        [HttpPost]
        public async Task<IActionResult> PostCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                await CoursesService.Insert(course);

                return CreatedAtAction(nameof(GetCourse), new { id = course.CourseID }, course);
            }
            return BadRequest();
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await CoursesService.Update(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetCourses));
            }
            return BadRequest();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await CoursesService.Delete(id);

            return RedirectToAction(nameof(GetCourses));
        }

        private bool CourseExists(int id)
        {
            return CoursesService.GetAll().Any(e => e.CourseID == id);
        }
    }
}
