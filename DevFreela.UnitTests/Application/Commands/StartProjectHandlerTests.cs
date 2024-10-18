using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ProjectStartedSuccessfully()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            var startCommand = new StartProjectCommand(1);

            // Simula um projeto válido retornado do repositório
            var project = new Project("Test Project", "Description", 1, 1, 1000);
            repository.GetById(startCommand.Id).Returns(project);

            var handler = new StartProjectHandler(repository);

            // Act
            var result = await handler.Handle(startCommand, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess); // Verifica se o resultado foi bem-sucedido
            Assert.True(string.IsNullOrWhiteSpace(result.Message)); // Verifica se não houve erro

            // Verifica se o método Start foi chamado no projeto
            Assert.True(project.Status==ProjectStatusEnum.InProgress);

            // Verifica se o método GetById foi chamado com o ID correto
            await repository.Received(1).GetById(startCommand.Id);

            // Verifica se o método Update foi chamado com o projeto atualizado
            await repository.Received(1).Update(project);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Executed_ReturnsError()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            var startCommand = new StartProjectCommand(1);

            // Simula que nenhum projeto foi encontrado
            repository.GetById(startCommand.Id).Returns((Project)null);

            var handler = new StartProjectHandler(repository);

            // Act
            var result = await handler.Handle(startCommand, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess); // Verifica se o resultado não foi bem-sucedido
            Assert.Equal("Projeto não existe.", result.Message); // Verifica se a mensagem de erro está correta

            // Verifica se o método GetById foi chamado com o ID correto
            await repository.Received(1).GetById(startCommand.Id);

            // Verifica que o método Update não foi chamado, já que o projeto não existe
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
