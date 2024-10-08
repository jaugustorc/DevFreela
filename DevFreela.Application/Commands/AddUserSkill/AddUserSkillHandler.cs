using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.AddUserSkill
{
    public class AddUserSkillHandler : IRequestHandler<AddUserSkillCommand, ResultViewModel<int>>
    {

        private readonly IUsersRepository _repository;
        public AddUserSkillHandler(IUsersRepository repository)
        {
            this._repository = repository;

        }

        public async Task<ResultViewModel<int>> Handle(AddUserSkillCommand request, CancellationToken cancellationToken)
        {
            // Add Skills
            var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

            await _repository.AddUserSkill(userSkills);

            return ResultViewModel<int>.Success(request.Id);
        }
    }
}