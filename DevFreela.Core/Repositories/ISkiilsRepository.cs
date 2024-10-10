using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface ISkillsRepository
    {
        Task<List<Skill>> GetAll();
        Task<Skill?> GetById(int id);
        Task<int> Add(Skill skill);
        Task<bool> Exists(int[] skills);
        Task<bool> Exists(int skill);


    }
}
