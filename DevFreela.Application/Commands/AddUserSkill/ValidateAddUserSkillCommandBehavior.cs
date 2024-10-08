using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.AddUserSkill
{
    class ValidateAddUserSkillCommandBehavior :
        IPipelineBehavior<AddUserSkillCommand, ResultViewModel<int>>
    {

        private readonly IUsersRepository _repository;
        private readonly ISkillsRepository _repositorySkill;
        public ValidateAddUserSkillCommandBehavior(IUsersRepository repository, ISkillsRepository repositorySkill)
        {
            this._repository = repository;
            this._repositorySkill = repositorySkill;
        }

        public async Task<ResultViewModel<int>> Handle(AddUserSkillCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            bool clientExists = await _repository.Exists(request.Id);

            bool skillsExists = await _repositorySkill.Exists(request.SkillIds);

            if (!clientExists || !skillsExists)
            {
                return ResultViewModel<int>.Error("Freelancer ou Skill inválidos.");
            }

            return await next();
        }
    }
}
