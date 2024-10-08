using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillsRepository
    {
        private readonly DevFreelaDbContext _context;
        public SkillRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            _context.SaveChanges();

            return skill.Id;
        }

        public async Task<bool> Exists(int[] skills)
        {
            var tasks = skills.Select(skillId => _context.Skills.AnyAsync(s => s.Id == skillId));
            var results = await Task.WhenAll(tasks);
            var exist = results.All(r => r);

            return exist;
        }

        public async Task<List<Skill>> GetAll()
        {
            var skills = await _context.Skills
                .Include(p => p.Description)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return skills;
        }

        public async Task<Skill?> GetById(int id)
        {
            return await _context.Skills
                .SingleOrDefaultAsync(p => p.Id == id);
        }

    }
}
