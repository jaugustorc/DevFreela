using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly ISkillService _service;
        public SkillsController(DevFreelaDbContext context,  ISkillService service)
        {
            _context = context;
            _service = service;
        }

        // GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        // POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var skill = _service.Insert(model);

            return NoContent();
        }
    }
}
