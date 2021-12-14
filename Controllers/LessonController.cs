using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("lessons/filter/")]
        public async Task<IActionResult> GetAsync([FromQuery] string instrument, [FromQuery] string level) {
            var lessons = await context.Lessons.ToListAsync();
            if(!string.IsNullOrEmpty(instrument))
                lessons = lessons.Where(x => x.Instrument == instrument).ToList();
            if(!string.IsNullOrEmpty(level))
                lessons = lessons.Where(x => x.Level == level).ToList();
            return Ok(lessons);
        }

        [HttpGet("lessons/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id) {
            var lesson = await context.Lessons.Include(x => x.Questions).ThenInclude(x => x.Alternatives).FirstOrDefaultAsync(x => x.Id == id);
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
                Genre = model.Genre,
                Level = model.Level,
                Article = model.Article,
                ArticleLegend = model.ArticleLegend,
                LinkVideo = model.LinkVideo,
                VideoLegend = model.VideoLegend,
                PerformanceExercise = model.PerformanceExercise,
                ExerciseLegend = model.ExerciseLegend,
                Questions = model.Questions
            };

            try {
                await context.Lessons.AddAsync(lesson);
                await context.SaveChangesAsync();
                return Created($"v1/lessons/{lesson.Id}", lesson);
            } catch (Exception ex) {
                Console.WriteLine(ex);
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
                lesson.Genre = model.Genre;
                lesson.Level = model.Level;
                lesson.Article = model.Article;
                lesson.ArticleLegend = model.ArticleLegend;
                lesson.LinkVideo = model.LinkVideo;
                lesson.VideoLegend = model.VideoLegend;
                lesson.PerformanceExercise = model.PerformanceExercise;
                lesson.ExerciseLegend = model.ExerciseLegend;
                lesson.Questions = model.Questions;
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