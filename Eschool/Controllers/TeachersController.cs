﻿using System;
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
    public class TeachersController : ControllerBase
    {
        private readonly EschoolContext _context;

        public TeachersController(EschoolContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(long id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(long id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            //Obtain a reference to the teacher to be updated  

            Teacher teacherToBeUpdated = _context.Teachers.Find(id);

            //Change only properties that are not null

            if (teacher.FirstName != null) teacherToBeUpdated.FirstName = teacher.FirstName;
            if (teacher.LastName != null) teacherToBeUpdated.LastName = teacher.LastName;

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(long id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(long id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }

        //GET: api/teachers?firstName=FirstNameVal&lastName=lastNameVal
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacherWithFilters([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return await _context.Teachers.Where(t => t.FirstName == firstName && t.LastName == lastName).ToListAsync();
        } */
    }
}
