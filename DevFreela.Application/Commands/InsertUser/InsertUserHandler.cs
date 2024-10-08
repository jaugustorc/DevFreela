using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;
        public InsertUserHandler(DevFreelaDbContext context, IMediator mediator)
        {
            this._context = context;
            this._mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
           var model = request.ToEntity();

            await _context.Users.AddAsync(model);
            _context.SaveChanges();
                       
            return ResultViewModel<int>.Success(model.Id);
        }
    }
}