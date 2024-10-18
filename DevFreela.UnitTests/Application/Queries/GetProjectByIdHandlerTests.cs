using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdHandlerTests
    {
        private readonly IProjectRepository _projectRepository;
        private readonly GetProjectByIdHandler _handler;

        public GetProjectByIdHandlerTests()
        {
            // Mocka o repositório usando o NSubstitute
            _projectRepository = Substitute.For<IProjectRepository>();

            // Inicializa o handler com o repositório mockado
            _handler = new GetProjectByIdHandler(_projectRepository);
        }

        [Fact]
        public async Task WhenProjectExists_Executed_ReturnsSuccessResult()
        {
            // Arrange: Configura o mock para retornar um projeto existente
            // Arrange
            User client = new("", "", DateTime.Now, "", "");
            User freela = new("", "", DateTime.Now, "", "");
            Project project = new("Título do Projeto", "Descrição", 1, 2, 10000);

            typeof(Project)
                .GetProperty("Client")
                .SetValue(project, client);
            typeof(Project)
                .GetProperty("Freelancer")
                .SetValue(project, freela);

            _projectRepository.GetDetailsById(Arg.Any<int>()).Returns(project);

            var request = new GetProjectByIdQuery(1);

            // Act: Executa o método Handle
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: Verifica se o repositório foi chamado
            await _projectRepository.Received(1).GetDetailsById(1);

            // Verifica se o resultado foi um sucesso e o projeto retornado é correto
            Assert.True(result.IsSuccess);
            Assert.Equal(project.Title, result.Data.Title);
            Assert.Equal(project.Description, result.Data.Description);
        }

        [Fact]
        public async Task WhenProjectDoesNotExist_Executed_ReturnsErrorResult()
        {
            // Arrange: Configura o mock para retornar null, simulando projeto não encontrado
            _projectRepository.GetDetailsById(Arg.Any<int>()).Returns((Project)null);

            var request = new GetProjectByIdQuery(1);

            // Act: Executa o método Handle
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: Verifica se o repositório foi chamado
            await _projectRepository.Received(1).GetDetailsById(1);

            // Verifica se o resultado foi um erro e a mensagem está correta
            Assert.False(result.IsSuccess);
            Assert.Equal("Projeto não existe.", result.Message);
        }
    }
}
