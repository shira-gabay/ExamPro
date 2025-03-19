using ExamPRO.API.Models;
using ExamPRO.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyMaterialController : ControllerBase
    {
        private readonly StudyMaterialService _studyMaterialService;

        public StudyMaterialController(StudyMaterialService studyMaterialService)
        {
            _studyMaterialService = studyMaterialService;
        }

        [HttpGet]
        public async Task<List<StudyMaterial>> Get() =>
            await _studyMaterialService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<StudyMaterial>> GetById(string id)
        {
            var studyMaterial = await _studyMaterialService.GetByIdAsync(id);
            if (studyMaterial is null) return NotFound();
            return studyMaterial;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudyMaterial studyMaterial)
        {
            await _studyMaterialService.CreateAsync(studyMaterial);
            return CreatedAtAction(nameof(GetById), new { id = studyMaterial.Id }, studyMaterial);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, StudyMaterial studyMaterial)
        {
            var existingMaterial = await _studyMaterialService.GetByIdAsync(id);
            if (existingMaterial is null) return NotFound();
            studyMaterial.Id = id;
            await _studyMaterialService.UpdateAsync(id, studyMaterial);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var studyMaterial = await _studyMaterialService.GetByIdAsync(id);
            if (studyMaterial is null) return NotFound();
            await _studyMaterialService.DeleteAsync(id);
            return NoContent();
        }
    }
}
