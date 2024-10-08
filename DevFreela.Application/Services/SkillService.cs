using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<SkillItemViewModel>> GetAll()
        {
            var skills = _context.Skills
                .Include(p => p.Description)
                .Where(p => !p.IsDeleted).ToList();

            var model = skills.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillItemViewModel>>.Success(model);
        }

        public ResultViewModel Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

    }
}
