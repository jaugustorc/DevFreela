using DevFreela.Application.Commands.InsertSkill;

using DevFreela.Application.Queries.GetAllSkills;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    
    public class SkillsController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        public SkillsController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        // GET api/skills
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSkillsQuery();

            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/skills
        [HttpPost]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Created();
        }
    }
}
