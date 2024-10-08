using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.AddUserSkill
{
    class ValidateAddUserSkillCommandBehavior :
        IPipelineBehavior<AddUserSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateAddUserSkillCommandBehavior(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(AddUserSkillCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _context.Users.Any(u => u.Id == request.Id);

            var skillsExists = request.SkillIds.All(skillId => _context.Skills.Any(s => s.Id == skillId));

            if (!clientExists || !skillsExists)
            {
                return ResultViewModel<int>.Error("Freelancer ou Skill inválidos.");
            }

            return await next();
        }
    }
}
