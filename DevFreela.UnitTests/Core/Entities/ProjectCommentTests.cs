using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.Core.Entities;
public class ProjectCommentTests
{
    [Fact]
    public void Constructor_WhenCalled_AssignsValuesCorrectly()
    {
        // Arrange: Definindo os valores de entrada para o construtor
        var content = "This is a comment.";
        var idProject = 1;
        var idUser = 2;

        // Act: Criando uma instância de ProjectComment
        var comment = new ProjectComment(content, idProject, idUser);

        // Assert: Verificando se os valores foram atribuídos corretamente
        Assert.Equal(content, comment.Content);
        Assert.Equal(idProject, comment.IdProject);
        Assert.Equal(idUser, comment.IdUser);
        Assert.Null(comment.Project);  // Projeto deve ser null ao criar o comentário
        Assert.Null(comment.User);     // Usuário deve ser null ao criar o comentário
    }
}
