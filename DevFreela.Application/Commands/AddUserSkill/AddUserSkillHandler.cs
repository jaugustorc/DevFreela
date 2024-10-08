using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.AddUserSkill
{
    public class InsertUserHandler : IRequestHandler<AddUserSkillCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
      
        public InsertUserHandler(DevFreelaDbContext context)
        {
            _context = context;
            
        }

        public async Task<ResultViewModel<int>> Handle(AddUserSkillCommand request, CancellationToken cancellationToken)
        {
           
            // Usuário existe ?
            var result = await _context.Users.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (result is null)
                return ResultViewModel<int>.Error("Usuário não existe.");

            // Add Skills
            var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _context.UserSkills.AddRangeAsync(userSkills);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(request.Id);
        }
    }
}