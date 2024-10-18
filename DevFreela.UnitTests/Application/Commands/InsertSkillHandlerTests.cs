using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Application.Models;
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
    public class InsertSkillHandlerTests
    {
        [Fact]
        public async Task ValidSkillData_Executed_ReturnsSuccess()
        {
            // Arrange
            var repository = Substitute.For<ISkillsRepository>();

            var insertSkillCommand = new InsertSkillCommand
            {
                // Definir propriedades necessárias para a skill
                Description = "Skill Test"
            };

            // Simula o mapeamento de InsertSkillCommand para uma entidade Skill
            var skill = new Skill (insertSkillCommand.Description);


            //insertSkillCommand.ToEntity().Returns(skill);

            var insertSkillHandler = new InsertSkillHandler(repository);

            // Act
            var result = (ResultViewModel<int>) await insertSkillHandler.Handle(insertSkillCommand, new CancellationToken());

            // Assert
            Assert.True(result.IsSuccess); // Verifica se o retorno foi de sucesso
            Assert.True(result.Data >= 0);

            // Verifica se o método Add foi chamado com a entidade correta
            await repository.Received(1).Add(Arg.Is<Skill>(s =>
                s.Description == insertSkillCommand.Description
            ));
        }
    }
}
