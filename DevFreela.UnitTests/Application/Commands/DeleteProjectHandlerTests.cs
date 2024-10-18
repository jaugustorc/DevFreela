using DevFreela.Application.Commands.DeleteProject;
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
    public class DeleteProjectHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnSuccess()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var deleteProjectCommand = new DeleteProjectCommand(1);

            Project project = new("Nome Do Teste 1", "Descrição De Teste 1", 1, 2, 10000);
            repository.GetById(deleteProjectCommand.Id).Returns(project); // Simula a busca do projeto no repositório

            var deleteProjectHandler = new DeleteProjectHandler(repository);

            // Act
            var response = await deleteProjectHandler.Handle(deleteProjectCommand, new CancellationToken());

            // Assert
            Assert.True(response.IsSuccess); // Verifica se o retorno foi de sucesso

            // Verifica se o método SetAsDeleted foi chamado no projeto
            Assert.True(project.IsDeleted); // Supondo que Project tenha uma propriedade IsDeleted

            // Verifica se o método Update foi chamado com o projeto correto
            await repository.Received(1).Update(project);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Executed_ReturnError()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var deleteProjectCommand = new DeleteProjectCommand(1);

            repository.GetById(deleteProjectCommand.Id).Returns((Project)null); // Simula projeto não encontrado

            var deleteProjectHandler = new DeleteProjectHandler(repository);

            // Act
            var response = await deleteProjectHandler.Handle(deleteProjectCommand, new CancellationToken());

            // Assert
            Assert.False(response.IsSuccess); // Verifica que o retorno foi um erro
            Assert.True(!string.IsNullOrWhiteSpace(response.Message));

            // Verifica que o método Update não foi chamado, pois o projeto não existe
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
