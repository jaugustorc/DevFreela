using DevFreela.Application.Commands;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task ValidLogin_Executed_ReturnsLoginUserViewModel()
        {
            // Arrange
            var authService = Substitute.For<IAuthService>();
            var userRepository = Substitute.For<IUsersRepository>();

            var loginCommand = new LoginUserCommand
            {
                Email = "john@example.com",
                Password = "password123"
            };

            // Simula a hash da senha gerada pelo IAuthService
            var passwordHash = "hashed_password";
            authService.ComputeSha256Hash(loginCommand.Password).Returns(passwordHash);

            // Simula um usuário retornado do banco
            var user = new User("John Doe", loginCommand.Email, new DateTime(1990, 1, 1), passwordHash, "User");
            userRepository.GetUserByEmailAndPasswordAsync(loginCommand.Email, passwordHash).Returns(user);

            // Simula a geração do token JWT
            var token = "generated_jwt_token";
            authService.GenerateJwtToken(user.Email, user.Role).Returns(token);

            var loginHandler = new LoginUserCommandHandler(authService, userRepository);

            // Act
            var result = await loginHandler.Handle(loginCommand, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess); // Verifica se o resultado foi bem-sucedido
            Assert.Equal(user.Email, result.Data.Email); // Verifica se o email retornado está correto
            Assert.Equal(token, result.Data.Token); // Verifica se o token gerado está correto

            // Verifica se o método ComputeSha256Hash foi chamado com a senha correta
            authService.Received(1).ComputeSha256Hash(loginCommand.Password);

            // Verifica se o método GetUserByEmailAndPasswordAsync foi chamado com o email e senha corretos
            await userRepository.Received(1).GetUserByEmailAndPasswordAsync(loginCommand.Email, passwordHash);

            // Verifica se o método GenerateJwtToken foi chamado com o email e a role do usuário
            authService.Received(1).GenerateJwtToken(user.Email, user.Role);
        }

        [Fact]
        public async Task InvalidLogin_Executed_ReturnsError()
        {
            // Arrange
            var authService = Substitute.For<IAuthService>();
            var userRepository = Substitute.For<IUsersRepository>();

            var loginCommand = new LoginUserCommand
            {
                Email = "john@example.com",
                Password = "wrongpassword"
            };

            // Simula a hash da senha gerada pelo IAuthService
            var passwordHash = "hashed_wrong_password";
            authService.ComputeSha256Hash(loginCommand.Password).Returns(passwordHash);

            // Simula que nenhum usuário foi encontrado no banco de dados
            userRepository.GetUserByEmailAndPasswordAsync(loginCommand.Email, passwordHash).Returns((User)null);

            var loginHandler = new LoginUserCommandHandler(authService, userRepository);

            // Act
            var result = await loginHandler.Handle(loginCommand, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess); // Verifica se o resultado foi um erro
            Assert.Equal("Wrong login or password", result.Message); // Verifica se a mensagem de erro está correta

            // Verifica se o método ComputeSha256Hash foi chamado com a senha correta
            authService.Received(1).ComputeSha256Hash(loginCommand.Password);

            // Verifica se o método GetUserByEmailAndPasswordAsync foi chamado com o email e senha corretos
            await userRepository.Received(1).GetUserByEmailAndPasswordAsync(loginCommand.Email, passwordHash);

            // Verifica que o método GenerateJwtToken não foi chamado, já que o login falhou
            authService.DidNotReceive().GenerateJwtToken(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
