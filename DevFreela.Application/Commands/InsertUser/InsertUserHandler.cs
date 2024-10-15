using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthService _authService;
        public InsertUserHandler(IUsersRepository repository, IAuthService authService)
        {
            this._repository = repository;
            this._authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity(this._authService);

            await this._repository.Add(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}