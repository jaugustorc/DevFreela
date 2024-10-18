using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Queries;
public class GetUserByIdHandlerTests
{
    private readonly IUsersRepository _userRepository;
    private readonly GetUserByIdHandler _handler;

    public GetUserByIdHandlerTests()
    {
        // Mocka o repositório usando NSubstitute
        _userRepository = Substitute.For<IUsersRepository>();

        // Inicializa o handler com o repositório mockado
        _handler = new GetUserByIdHandler(_userRepository);
    }

    [Fact]
    public async Task WhenUserExists_Executed_ReturnsSuccessResult()
    {
        // Arrange: Configura o mock para retornar um usuário existente
        var user = new User("Nome Completo", "email@dominio.com", new DateTime(1990, 1, 1), "hashSenha", "role");
        _userRepository.GetById(Arg.Any<int>()).Returns(user);

        var request = new GetUserByIdQuery(1);

        // Act: Executa o método Handle
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert: Verifica se o repositório foi chamado
        await _userRepository.Received(1).GetById(1);

        // Verifica se o resultado foi um sucesso e o usuário retornado é correto
        Assert.True(result.IsSuccess);
        Assert.Equal("Nome Completo", result.Data.FullName);
        Assert.Equal("email@dominio.com", result.Data.Email);
    }

    [Fact]
    public async Task WhenUserDoesNotExist_Executed_ReturnsErrorResult()
    {
        // Arrange: Configura o mock para retornar null, simulando usuário não encontrado
        _userRepository.GetById(Arg.Any<int>()).Returns((User)null);

        var request = new GetUserByIdQuery(1);

        // Act: Executa o método Handle
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert: Verifica se o repositório foi chamado
        await _userRepository.Received(1).GetById(1);

        // Verifica se o resultado foi um erro e a mensagem está correta
        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não existe.", result.Message);
    }
}