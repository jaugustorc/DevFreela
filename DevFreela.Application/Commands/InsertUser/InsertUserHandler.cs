using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUsersRepository _repository;
        public InsertUserHandler(IUsersRepository repository)
        {
            this._repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
           var model = request.ToEntity();

            await _repository.Add(model);

            return ResultViewModel<int>.Success(model.Id);
        }
    }
}