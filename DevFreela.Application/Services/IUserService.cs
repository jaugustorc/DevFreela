using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel Insert(CreateUserInputModel model);
        ResultViewModel AddSkill(int id, UserSkillsInputModel model);
        
    }
}
