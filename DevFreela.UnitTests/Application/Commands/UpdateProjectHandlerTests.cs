using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ProjectUpdatedSuccessfully()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            var updateCommand = new UpdateProjectCommand
            {
                IdProject = 1,
                Title = "New Title",
                Description = "Updated Description",
                TotalCost = 2000
            };

            // Simula um projeto válido retornado do repositório
            var project = new Project("Old Title", "Old Description", 1, 1, 1000);
            repository.GetById(updateCommand.IdProject).Returns(project);

            var handler = new UpdateProjectHandler(repository);

            // Act
            var result = await handler.Handle(updateCommand, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess); // Verifica se o resultado foi bem-sucedido
            Assert.True(string.IsNullOrWhiteSpace(result.Message)); // Verifica se não houve erro

            // Verifica se o projeto foi atualizado corretamente
            Assert.Equal(updateCommand.Title, project.Title);
            Assert.Equal(updateCommand.Description, project.Description);
            Assert.Equal(updateCommand.TotalCost, project.TotalCost);

            // Verifica se o método GetById foi chamado com o ID correto
            await repository.Received(1).GetById(updateCommand.IdProject);

            // Verifica se o método Update foi chamado com o projeto atualizado
            await repository.Received(1).Update(project);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Executed_ReturnsError()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            var updateCommand = new UpdateProjectCommand
            {
                IdProject = 1,
                Title = "New Title",
                Description = "Updated Description",
                TotalCost = 2000
            };

            // Simula que nenhum projeto foi encontrado
            repository.GetById(updateCommand.IdProject).Returns((Project)null);

            var handler = new UpdateProjectHandler(repository);

            // Act
            var result = await handler.Handle(updateCommand, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess); // Verifica se o resultado não foi bem-sucedido
            Assert.Equal("Projeto não existe.", result.Message); // Verifica se a mensagem de erro está correta

            // Verifica se o método GetById foi chamado com o ID correto
            await repository.Received(1).GetById(updateCommand.IdProject);

            // Verifica que o método Update não foi chamado, já que o projeto não existe
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
