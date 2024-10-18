using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using System;
using Xunit;

namespace DevFreela.Core.Entities;
public class ProjectTests
{
    [Fact]
    public void Start_WhenProjectIsCreated_ChangesStatusToInProgressAndSetsStartDate()
    {
        // Arrange: Cria um projeto com status "Created"
        var project = new Project("Title", "Description", 1, 2, 1000m);

        // Act: Chama o método Start
        project.Start();

        // Assert: Verifica se o status foi atualizado para "InProgress" e a data de início foi definida
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
    }

    [Fact]
    public void Complete_WhenProjectIsInProgress_ChangesStatusToCompletedAndSetsCompletedDate()
    {
        // Arrange: Cria um projeto e define o status como "InProgress"
        var project = new Project("Title", "Description", 1, 2, 1000m);
        project.Start();

        // Act: Chama o método Complete
        project.Complete();

        // Assert: Verifica se o status foi atualizado para "Completed" e a data de término foi definida
        Assert.Equal(ProjectStatusEnum.Completed, project.Status);
        Assert.NotNull(project.CompletedAt);
    }

    [Fact]
    public void Complete_WhenProjectIsPaymentPending_ChangesStatusToCompletedAndSetsCompletedDate()
    {
        // Arrange: Cria um projeto e define o status como "PaymentPending"
        var project = new Project("Title", "Description", 1, 2, 1000m);
        project.Start();
        project.SetPaymentPending();

        // Act: Chama o método Complete
        project.Complete();

        // Assert: Verifica se o status foi atualizado para "Completed" e a data de término foi definida
        Assert.Equal(ProjectStatusEnum.Completed, project.Status);
        Assert.NotNull(project.CompletedAt);
    }

    [Fact]
    public void Cancel_WhenProjectIsInProgressOrSuspended_ChangesStatusToCancelled()
    {
        // Arrange: Cria um projeto e define o status como "InProgress"
        var project = new Project("Title", "Description", 1, 2, 1000m);
        project.Start();

        // Act: Chama o método Cancel
        project.Cancel();

        // Assert: Verifica se o status foi atualizado para "Cancelled"
        Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
    }

    [Fact]
    public void SetPaymentPending_WhenProjectIsInProgress_ChangesStatusToPaymentPending()
    {
        // Arrange: Cria um projeto e define o status como "InProgress"
        var project = new Project("Title", "Description", 1, 2, 1000m);
        project.Start();

        // Act: Chama o método SetPaymentPending
        project.SetPaymentPending();

        // Assert: Verifica se o status foi atualizado para "PaymentPending"
        Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
    }

    [Fact]
    public void Update_WhenCalled_UpdatesTitleDescriptionAndTotalCost()
    {
        // Arrange: Cria um projeto inicial
        var project = new Project("Old Title", "Old Description", 1, 2, 1000m);

        // Act: Chama o método Update
        project.Update("New Title", "New Description", 2000m);

        // Assert: Verifica se o título, a descrição e o custo total foram atualizados
        Assert.Equal("New Title", project.Title);
        Assert.Equal("New Description", project.Description);
        Assert.Equal(2000m, project.TotalCost);
    }
}