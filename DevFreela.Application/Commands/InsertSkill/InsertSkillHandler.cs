using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly ISkillsRepository _repository;
        public InsertSkillHandler(ISkillsRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
           var model = request.ToEntity();

            await _repository.Add(model);

            return ResultViewModel<int>.Success(model.Id);
        }
    }
}