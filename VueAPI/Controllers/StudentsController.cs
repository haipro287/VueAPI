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
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService StudentsService;

        public StudentsController(StudentsService StudentsService)
        {
            this.StudentsService = StudentsService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await StudentsService.GetAll().ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await StudentsService.Detail(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await StudentsService.Insert(student);

                    return CreatedAtAction(nameof(GetStudent), new { id = student.ID }, student);
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log)
                ModelState.AddModelError("", "Unable to saves changes. " +
                    "Try again, and if the problems persists " +
                    "see your system administrator");
            }
            return BadRequest();
        }

        // PUT: Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await StudentsService.Update(student);

                    return NoContent();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return BadRequest();
        }

        // DELETE: Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = StudentsService.GetByID(id);
            if (student == null)
            {
                return NotFound();
            }

            try
            {
                await StudentsService.Delete(id);

                return RedirectToAction(nameof(GetStudents));
            }
            catch (DbUpdateException /* ex */)
            {
                //log the error (uncomment ex variable name and write a log.)
                return BadRequest();
            }
        }

        private bool StudentExists(int id)
        {
            return StudentsService.GetAll().Any(e => e.ID == id);
        }
    }
}
