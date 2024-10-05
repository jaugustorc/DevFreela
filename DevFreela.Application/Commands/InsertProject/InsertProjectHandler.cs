using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;
        public InsertProjectHandler(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _context.Projects.AddAsync(project);
            _context.SaveChanges();

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated);

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}