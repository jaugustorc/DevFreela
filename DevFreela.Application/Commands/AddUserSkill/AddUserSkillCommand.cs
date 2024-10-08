using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.AddUserSkill
{
    public class AddUserSkillCommand : IRequest<ResultViewModel<int>>
    {

        public int[] SkillIds { get; set; }
        public int Id { get; set; }
                
    }
}
