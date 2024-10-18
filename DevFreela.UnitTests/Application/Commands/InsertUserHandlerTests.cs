using DevFreela.Application.Commands.InsertUser;
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
    public class InsertUserHandlerTests
    {
        [Fact]
        public async Task ValidUserData_Executed_ReturnsUserId()
        {
            // Arrange
            var repository = Substitute.For<IUsersRepository>();
            var authService = Substitute.For<IAuthService>();

            var insertUserCommand = new InsertUserCommand
            {
                FullName = "John Doe",
                Password = "password123",
                Email = "john@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                Role = "User"
            };

            // Simula a hash da senha gerada pelo IAuthService
            var hashedPassword = "hashed_password";
            authService.ComputeSha256Hash(insertUserCommand.Password).Returns(hashedPassword);

            // Simula a criação da entidade User a partir do comando
            var user = new User(insertUserCommand.FullName, insertUserCommand.Email, insertUserCommand.BirthDate, hashedPassword, insertUserCommand.Role);
            repository.Add(user).Returns(1);
            
            var insertUserHandler = new InsertUserHandler(repository, authService);

            // Act
            var result = await insertUserHandler.Handle(insertUserCommand, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess); // Verifica se o retorno foi de sucesso
            Assert.True(result.Data >= 0);

            // Verifica se o método ComputeSha256Hash foi chamado com a senha correta
            authService.Received(1).ComputeSha256Hash(insertUserCommand.Password);

            // Verifica se o método Add foi chamado com a entidade User correta
            await repository.Received(1).Add(Arg.Is<User>(u =>
                u.FullName == insertUserCommand.FullName &&
                u.Email == insertUserCommand.Email &&
                u.BirthDate == insertUserCommand.BirthDate &&
                u.Password == hashedPassword && // Verifica se a senha foi hashada corretamente
                u.Role == insertUserCommand.Role
            ));
        }
    }
}
