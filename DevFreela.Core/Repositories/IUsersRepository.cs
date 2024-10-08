using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<int> Add(User user);
        Task<bool> Exists(int id);
        Task AddUserSkill(List<UserSkill> userSkills);

    }
}
