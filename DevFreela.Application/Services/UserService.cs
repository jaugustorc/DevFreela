using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }

        
        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                .Include(p => p.Skills)
                .ThenInclude(u => u.Skill)
                .SingleOrDefault(p => p.Id == id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Error("Usuário não existe.");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel Insert(CreateUserInputModel model)
        {
            var project = model.ToEntity();

            _context.Users.Add(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel AddSkill(int id, UserSkillsInputModel model)
        {
            // Usuário existe ?
            var result = this.GetById(id);

            if(!result.IsSuccess)
                return ResultViewModel.Error("Usuário não existe.");

            // Add Skills
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
        
    }
}
