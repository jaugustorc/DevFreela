using DevFreela.Application.Commands.AddUserSkill;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using System.Linq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class AddUserSkillHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            // Arrange
            var repository = Substitute.For<IUsersRepository>();
            
            var addUserSkillCommand = new AddUserSkillCommand()
            {
                SkillIds = [0 ,1],
                Id= 0
            };

            var addUserSkillCommandHandler = new AddUserSkillHandler(repository);

            // Act
            var response = await addUserSkillCommandHandler.Handle(addUserSkillCommand, new CancellationToken());

            // Assert
            Assert.True(response.Data == addUserSkillCommand.Id);

            // Verifica se o método AddUserSkill foi chamado com a lista correta de UserSkill
            await repository.Received(1).AddUserSkill(Arg.Is<List<UserSkill>>(us =>
                us.Count == 2 &&  // Verifica se há 2 habilidades (pois SkillIds tem 2 itens)
                us.All(s => s.IdUser == addUserSkillCommand.Id) &&  // Verifica se todas as habilidades têm o UserId correto
                us.Select(s => s.IdSkill).SequenceEqual(addUserSkillCommand.SkillIds) // Verifica se os SkillIds são iguais
            ));
        }
    }
}
