using DevFreela.Application.Commands.InsertComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class InsertCommentHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Executed_ReturnSuccess()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var insertCommentCommand = new InsertCommentCommand
            {
                Content = "Novo comentário",
                IdProject = 1,
                IdUser = 1
            };

            // Simula que o projeto existe
            repository.Exists(insertCommentCommand.IdProject).Returns(true);

            var insertCommentHandler = new InsertCommentHandler(repository);

            // Act
            var response = await insertCommentHandler.Handle(insertCommentCommand, new CancellationToken());

            // Assert
            Assert.True(response.IsSuccess); // Verifica se o retorno foi de sucesso

            // Verifica se o método AddComment foi chamado com o comentário correto
            await repository.Received(1).AddComment(Arg.Is<ProjectComment>(comment =>
                comment.Content == insertCommentCommand.Content &&
                comment.IdProject == insertCommentCommand.IdProject &&
                comment.IdUser == insertCommentCommand.IdUser
            ));
        }

        [Fact]
        public async Task ProjectDoesNotExist_Executed_ReturnError()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();

            var insertCommentCommand = new InsertCommentCommand
            {
                Content = "Comentário inválido",
                IdProject = 1,
                IdUser = 1
            };

            // Simula que o projeto não existe
            repository.Exists(insertCommentCommand.IdProject).Returns(false);

            var insertCommentHandler = new InsertCommentHandler(repository);

            // Act
            var response = await insertCommentHandler.Handle(insertCommentCommand, new CancellationToken());

            // Assert
            Assert.False(response.IsSuccess); // Verifica que o retorno foi um erro
            Assert.True(!string.IsNullOrWhiteSpace(response.Message)); // Verifica a mensagem de erro

            // Verifica que o método AddComment não foi chamado, pois o projeto não existe
            await repository.DidNotReceive().AddComment(Arg.Any<ProjectComment>());
        }
    }
}
