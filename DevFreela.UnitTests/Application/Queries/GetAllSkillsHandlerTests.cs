using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsHandlerTests
    {
        private readonly ISkillsRepository _skillsRepository;
        private readonly GetAllSkillsHandler _handler;

        public GetAllSkillsHandlerTests()
        {
            // Mocka o repositório usando o NSubstitute
            _skillsRepository = Substitute.For<ISkillsRepository>();

            // Inicializa o handler com o repositório mockado
            _handler = new GetAllSkillsHandler(_skillsRepository);
        }

        [Fact]
        public async Task WhenSkillsExist_Executed_ReturnsSuccessResult()
        {
            // Arrange: Configura o mock para retornar uma lista de skills
            var skills = new List<Skill>
        {
            new Skill("C#"),
            new Skill("Python")
        };

            _skillsRepository.GetAll().Returns(skills);

            var request = new GetAllSkillsQuery();

            // Act: Executa o método Handle
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: Verifica se o repositório foi chamado
            await _skillsRepository.Received(1).GetAll();

            // Verifica se o resultado foi um sucesso e contém a lista de skills
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("C#", result.Data[0].Description);
            Assert.Equal("Python", result.Data[1].Description);
        }

        [Fact]
        public async Task WhenNoSkillsExist_Executed_ReturnsEmptyList()
        {
            // Arrange: Configura o mock para retornar uma lista vazia
            _skillsRepository.GetAll().Returns(new List<Skill>());

            var request = new GetAllSkillsQuery();

            // Act: Executa o método Handle
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: Verifica se o repositório foi chamado
            await _skillsRepository.Received(1).GetAll();

            // Verifica se o resultado foi um sucesso e a lista está vazia
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
        }
    }
}
