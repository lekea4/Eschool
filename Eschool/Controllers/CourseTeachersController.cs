using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eschool.Models;

namespace Eschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseTeachersController : ControllerBase
    {
        private readonly EschoolContext _context;

        public CourseTeachersController(EschoolContext context)
        {
            _context = context;
        }

        // GET: api/CourseTeachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseTeacher>>> GetCourseTeachers()
        {
            return await _context.CourseTeachers.ToListAsync();
        }

        // GET: api/CourseTeachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseTeacher>> GetCourseTeacher(long id)
        {
            var courseTeacher = await _context.CourseTeachers.FindAsync(id);

            if (courseTeacher == null)
            {
                return NotFound();
            }

            return courseTeacher;
        }

        // PUT: api/CourseTeachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseTeacher(long id, CourseTeacher courseTeacher)
        {
            if (id != courseTeacher.TeacherId)
            {
                return BadRequest();
            }

            _context.Entry(courseTeacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseTeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CourseTeachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseTeacher>> PostCourseTeacher(CourseTeacher courseTeacher)
        {
            _context.CourseTeachers.Add(courseTeacher);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseTeacherExists(courseTeacher.TeacherId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourseTeacher", new { id = courseTeacher.TeacherId }, courseTeacher);
        }

        // DELETE: api/CourseTeachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseTeacher(long id)
        {
            var courseTeacher = await _context.CourseTeachers.FindAsync(id);
            if (courseTeacher == null)
            {
                return NotFound();
            }

            _context.CourseTeachers.Remove(courseTeacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseTeacherExists(long id)
        {
            return _context.CourseTeachers.Any(e => e.TeacherId == id);
        }
    }
}
