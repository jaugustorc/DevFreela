using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IProjectService _service;
        public ProjectsController(DevFreelaDbContext context, IProjectService service)
        {
            _context = context;
            _service = service;
        }

        // GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        // GET api/projects/1234
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

        // POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _service.Insert(model);

             return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/projects/1234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var result = _service.Update(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        //  DELETE api/projects/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _service.Start(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // PUT api/projects/1234/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _service.Complete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var result = _service.InsertComment(id, model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
