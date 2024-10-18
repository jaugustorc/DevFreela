using Xunit;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevFreela.Core.Entities;
public class UserTests
{
    [Fact]
    public void Constructor_WhenCalled_AssignsValuesCorrectly()
    {
        // Arrange: Definindo os dados do usuário
        var fullName = "John Doe";
        var email = "john.doe@example.com";
        var birthDate = new DateTime(1990, 1, 1);
        var password = "securepassword";
        var role = "Freelancer";

        // Act: Criando uma instância de User
        var user = new User(fullName, email, birthDate, password, role);

        // Assert: Verificando se os atributos foram atribuídos corretamente
        Assert.Equal(fullName, user.FullName);
        Assert.Equal(email, user.Email);
        Assert.Equal(birthDate, user.BirthDate);
        Assert.True(user.Active); // Verificando se o usuário está ativo
        Assert.Equal(password, user.Password);
        Assert.Equal(role, user.Role);

        // Verificando se as listas foram inicializadas corretamente
        Assert.NotNull(user.Skills);
        Assert.Empty(user.Skills); // Deve estar vazia após a criação

        Assert.NotNull(user.OwnedProjects);
        Assert.Empty(user.OwnedProjects); // Deve estar vazia após a criação

        Assert.NotNull(user.FreelanceProjects);
        Assert.Empty(user.FreelanceProjects); // Deve estar vazia após a criação

        Assert.NotNull(user.Comments);
        Assert.Empty(user.Comments); // Deve estar vazia após a criação
    }
}
