using ExamPRO.API.Models;
using ExamPRO.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectCategoryController : ControllerBase
    {
        private readonly SubjectCategoryService _subjectCategoryService;

        public SubjectCategoryController(SubjectCategoryService subjectCategoryService)
        {
            _subjectCategoryService = subjectCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubjectCategory>>> Get() =>
            await _subjectCategoryService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SubjectCategory>> GetById(string id)
        {
            var category = await _subjectCategoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return category;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectCategory category)
        {
            await _subjectCategoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, SubjectCategory category)
        {
            var existingCategory = await _subjectCategoryService.GetByIdAsync(id);
            if (existingCategory == null) return NotFound();

            category.Id = id;
            await _subjectCategoryService.UpdateAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingCategory = await _subjectCategoryService.GetByIdAsync(id);
            if (existingCategory == null) return NotFound();

            await _subjectCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
