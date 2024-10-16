using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            User client = new("", "", DateTime.Now, "", "");
            User freela = new("", "", DateTime.Now, "", "");
            Project project = new("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000);

            typeof(Project)
                .GetProperty("Client")
                .SetValue(project, client);
            typeof(Project)
                .GetProperty("Freelancer")
                .SetValue(project, freela);


            var projects = new List<Project>
                { project, project, project };

            var projectRepository = Substitute.For<IProjectRepository>();
            projectRepository.GetAll().Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery();
            var getAllProjectsQueryHandler = new GetAllProjectsHandler(projectRepository);

            // Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList.Data);
            Assert.Equal(projects.Count, projectViewModelList.Data.Count);

            // Verifica se o método GetAllAsync foi chamado uma vez
            await projectRepository.Received(1).GetAll();
        }
    }
}
