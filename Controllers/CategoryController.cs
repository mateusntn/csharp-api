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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext context;
        public CategoryController (AppDbContext _context) {
            context = _context;
        }

        [HttpGet("categorys")]
        public async Task<IActionResult> GetAsync() {
            var categorys = await context.Categorys.ToListAsync();
            return Ok(categorys);
        }

        [HttpGet("categorys/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id) {
            var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);
            return category != null ? Ok(category) : NotFound();
        }

        [HttpPost("categorys")]
        public async Task<IActionResult> CreateAsync([FromBody] Category model) {
            if(!ModelState.IsValid)
                return BadRequest();

            var category = new Category {
                Name = model.Name,
                Icon = model.Icon,
                CreatedAt = DateTime.Now,
                LastUpdateDate = DateTime.Now,
            };

            try {
                await context.Categorys.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categorys/{category.Id}", category);
            } catch {
                return BadRequest();
            }          
        }

        [HttpPut("categorys/{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Category model, [FromRoute]int id) {
            if(!ModelState.IsValid)
                return BadRequest();

            var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            try {
                category.Name = model.Name;
                category.Icon = model.Icon;
                context.Categorys.Update(category);
                await context.SaveChangesAsync();
                return Ok(category);
            } catch {
                return BadRequest();
            }          
        }

        [HttpDelete("categorys/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id) {

            var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            try {
                context.Categorys.Remove(category);
                await context.SaveChangesAsync();
                return Ok(category);
            } catch {
                return BadRequest();
            }          
        }
    }
}