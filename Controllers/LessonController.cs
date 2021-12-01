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

        // [HttpPut("categorys/{id}")]
        // public async Task<IActionResult> UpdateAsync([FromBody] Category model, [FromRoute]int id) {
        //     if(!ModelState.IsValid)
        //         return BadRequest();

        //     var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);

        //     if (category == null)
        //         return NotFound();

        //     try {
        //         category.Name = model.Name;
        //         category.Icon = model.Icon;
        //         context.Categorys.Update(category);
        //         await context.SaveChangesAsync();
        //         return Ok(category);
        //     } catch {
        //         return BadRequest();
        //     }          
        // }

        // [HttpDelete("categorys/{id}")]
        // public async Task<IActionResult> DeleteAsync([FromRoute]int id) {

        //     var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);

        //     if (category == null)
        //         return NotFound();

        //     try {
        //         context.Categorys.Remove(category);
        //         await context.SaveChangesAsync();
        //         return Ok(category);
        //     } catch {
        //         return BadRequest();
        //     }          
        // }
    }
}