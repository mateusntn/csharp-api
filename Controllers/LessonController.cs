using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LessonController : ControllerBase
    {
        private readonly AppDbContext context;
        public LessonController (AppDbContext _context) {
            context = _context;
        }

        [HttpGet("lessons")]
        public async Task<IActionResult> GetAsync() {
            var lessons = await context.Lessons.ToListAsync();
            return Ok(lessons);
        }

        [HttpGet("lessons/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id) {
            var lesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
            return lesson != null ? Ok(lesson) : NotFound();
        }

        [HttpPost("lessons")]
        public async Task<IActionResult> CreateAsync([FromBody] Lesson model) {
            if(!ModelState.IsValid)
                return BadRequest();

            var lesson = new Lesson {
                Title = model.Title,
                Description = model.Description,
                Instrument = model.Instrument,
                Author = model.Author,
            };

            try {
                await context.Lessons.AddAsync(lesson);
                await context.SaveChangesAsync();
                return Created($"v1/lessons/{lesson.Id}", lesson);
            } catch {
                return BadRequest();
            }          
        }

        [HttpPut("lessons/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Lesson model, [FromRoute]int id) {
            if(!ModelState.IsValid)
                return BadRequest();

            var lesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == id);

            if (lesson == null)
                return NotFound();

            try {
                lesson.Title = model.Title;
                lesson.Description = model.Description;
                lesson.Instrument = model.Instrument;
                lesson.Author = model.Author;
                context.Lessons.Update(lesson);
                await context.SaveChangesAsync();
                return Ok(lesson);
            } catch {
                return BadRequest();
            }          
        }

        [HttpDelete("lessons/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id) {

            var lesson = await context.Lessons.FirstOrDefaultAsync(x => x.Id == id);

            if (lesson == null)
                return NotFound();

            try {
                context.Lessons.Remove(lesson);
                await context.SaveChangesAsync();
                return Ok(lesson);
            } catch {
                return BadRequest();
            }          
        }
    }
}