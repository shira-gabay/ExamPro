using ExamPRO.API.Models;
using ExamPRO.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;

        public ExamController(ExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<List<Exam>> Get() =>
            await _examService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Exam>> GetById(string id)
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam is null) return NotFound();
            return exam;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Exam exam)
        {
            await _examService.CreateAsync(exam);
            return CreatedAtAction(nameof(GetById), new { id = exam.Id }, exam);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Exam exam)
        {
            var existingExam = await _examService.GetByIdAsync(id);
            if (existingExam is null) return NotFound();
            exam.Id = id;
            await _examService.UpdateAsync(id, exam);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam is null) return NotFound();
            await _examService.DeleteAsync(id);
            return NoContent();
        }
    }
}
