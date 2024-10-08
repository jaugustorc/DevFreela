using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<SkillItemViewModel>> GetAll();
        ResultViewModel Insert(CreateSkillInputModel model);
        
    }
}
