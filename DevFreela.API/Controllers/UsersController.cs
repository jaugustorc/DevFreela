using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public UsersController(DevFreelaDbContext context, IUserService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);         
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var result = _service.Insert(model);

            return NoContent();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var result = _service.AddSkill(id,model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();

        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(int id, IFormFile file)
        {
            var description = $"FIle: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
    }
}
