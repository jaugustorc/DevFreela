using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class InsertProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectRepository = Substitute.For<IProjectRepository>();
            var mediatoR = Substitute.For<IMediator>();

            var createProjectCommand = new InsertProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Uma descrição Daora",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new InsertProjectHandler(mediatoR, projectRepository);

            // Act
            var response = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // Assert
            Assert.True(response.Data >= 0);

            await projectRepository.Received(1).Add(Arg.Any<Project>());
        }
    }
}
