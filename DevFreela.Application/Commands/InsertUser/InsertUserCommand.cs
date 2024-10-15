using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel<int>>
    {

        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }

        public User ToEntity(IAuthService _authService)  
            => new User(this.FullName, this.Email, this.BirthDate, _authService.ComputeSha256Hash(this.Password), this.Role);
    }
}
