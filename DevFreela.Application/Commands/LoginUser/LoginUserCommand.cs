using DevFreela.Application.Models;

using MediatR;

namespace DevFreela.Application.Commands
{
    public class LoginUserCommand : IRequest<ResultViewModel<LoginUserViewModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
