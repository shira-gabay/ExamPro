using ExamPRO.API.Models;
using ExamPRO.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamPRO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var existingUser = await _userService.GetByIdAsync(id);
            if (existingUser is null) return NotFound();
            user.Id = id;
            await _userService.UpdateAsync(id, user);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null) return NotFound();
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
