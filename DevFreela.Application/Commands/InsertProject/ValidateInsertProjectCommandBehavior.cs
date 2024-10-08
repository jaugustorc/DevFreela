using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertProjectCommandBehavior :
        IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        
        private readonly IUsersRepository _repository;

        public ValidateInsertProjectCommandBehavior(IUsersRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = await _repository.Exists(request.IdClient);
            var freelancerExists = await _repository.Exists(request.IdFreelancer);

            if (!clientExists || !freelancerExists)
            {
                return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos.");
            }

            return await next();
        }
    }
}
