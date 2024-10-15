using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginUserViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUsersRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUsersRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Utilizar o mesmo algoritmo para criar o hash dessa senha
            var passwordHash = this._authService.ComputeSha256Hash(request.Password);

            // Buscar no meu banco de dados um User que tenha meu e-mail e minha senha em formato hash
            var user = await this._userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            // Se não existir, erro no login
            if (user == null)
            {
                return ResultViewModel<LoginUserViewModel>.Error("Wrong login or password");
            }

            // Se existir, gero o token usando os dados do usuário
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return ResultViewModel<LoginUserViewModel>.Success(new LoginUserViewModel(user.Email, token));
        }
    }
}
