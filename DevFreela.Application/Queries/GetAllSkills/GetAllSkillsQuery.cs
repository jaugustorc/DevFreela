using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<ResultViewModel<List<SkillItemViewModel>>>
    {
        
    }
}
