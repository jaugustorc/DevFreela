using Xunit;
using DevFreela.Core.Entities;
using System.Collections.Generic;

namespace DevFreela.Core.Entities;
public class SkillTests
{
    [Fact]
    public void Constructor_WhenCalled_AssignsValuesCorrectly()
    {
        // Arrange: Definindo a descrição da habilidade
        var description = "C#";

        // Act: Criando uma instância de Skill
        var skill = new Skill(description);

        // Assert: Verificando se a descrição foi atribuída corretamente
        Assert.Equal(description, skill.Description);

        // Verificando se a lista de UserSkills foi inicializada corretamente
        Assert.NotNull(skill.UserSkills);
        Assert.Empty(skill.UserSkills); // Deve estar vazia após a criação
    }
}
