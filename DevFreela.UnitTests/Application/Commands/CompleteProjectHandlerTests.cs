using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CompleteProjectHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnSuccessStatusCompleted()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var completeProjectCommand = new CompleteProjectCommand(1);

            User client = new("", "", DateTime.Now, "", "");
            User freela = new("", "", DateTime.Now, "", "");
            Project project = new("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000);

            typeof(Project)
                .GetProperty("Client")
                .SetValue(project, client);
            typeof(Project)
                .GetProperty("Status")
                .SetValue(project, ProjectStatusEnum.InProgress);


            repository.GetById(completeProjectCommand.Id).Returns(project); // Simula a busca do projeto no repositório

            var completeProjectHandler = new CompleteProjectHandler(repository);

            // Act
            var response = await completeProjectHandler.Handle(completeProjectCommand, new CancellationToken());

            // Assert
            Assert.True(response.IsSuccess); // Verifica se o retorno foi de sucesso

            // Verifica se o método Update foi chamado uma vez com o projeto correto
            await repository.Received(1).Update(project);

            // Verifica se o método Complete foi chamado
            Assert.True(project.Status == ProjectStatusEnum.Completed);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Executed_ReturnError()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var completeProjectCommand = new CompleteProjectCommand(1);

            repository.GetById(completeProjectCommand.Id).Returns((Project)null); // Simula projeto não encontrado

            var completeProjectHandler = new CompleteProjectHandler(repository);

            // Act
            var response = await completeProjectHandler.Handle(completeProjectCommand, new CancellationToken());

            // Assert
            Assert.False(response.IsSuccess); // Verifica que o retorno foi um erro
            Assert.True(!string.IsNullOrWhiteSpace(response.Message));
            
            // Verifica que o método Update não foi chamado, pois o projeto não existe
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
