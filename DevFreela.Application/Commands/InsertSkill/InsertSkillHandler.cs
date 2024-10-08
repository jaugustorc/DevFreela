using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertSkill
{
    public class InsertSkillHandler : IRequestHandler<InsertSkillCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;
        public InsertSkillHandler(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultViewModel> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
           var model = request.ToEntity();

            await _context.Skills.AddAsync(model);
            _context.SaveChanges();
                       
            return ResultViewModel<int>.Success(model.Id);
        }
    }
}