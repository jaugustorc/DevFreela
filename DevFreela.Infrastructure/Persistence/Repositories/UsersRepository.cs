using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
        public class UsersRepository : IUsersRepository
    {
        private readonly DevFreelaDbContext _context;
        public UsersRepository(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();

            return user.Id;
        }

        public async Task AddUserSkill(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            _context.SaveChanges();

            return;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Users.AnyAsync(p => p.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            var skills = await _context.Users
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return skills;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .Include(u=>u.Skills)
                .ThenInclude(u=>u.Skill)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await this._context.Users
                .SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }
    }
}
