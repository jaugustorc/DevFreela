using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Criando painel para o projeto {notification.Title}");

            return Task.CompletedTask;
        }
    }
}
