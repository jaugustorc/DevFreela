using DevFreela.Application.Commands.InsertComment;
using DevFreela.Core.Repositories;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertCommentValidator : AbstractValidator<InsertCommentCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUsersRepository _userRepository;
        public InsertCommentValidator(IProjectRepository projectRepository, IUsersRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;

            // Validação para 'Content' (não nulo, tamanho mínimo e máximo)
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty.")
                .MinimumLength(5).WithMessage("Content must be at least 5 characters long.")
                .MaximumLength(500).WithMessage("Content cannot exceed 500 characters.");

            // Validação para 'IdProject' (maior que zero e verificar existência no banco)
            RuleFor(x => x.IdProject)
                .GreaterThan(0).WithMessage("IdProject must be greater than 0.")
                .MustAsync(async (idProject, cancellation) => await _projectRepository.Exists(idProject))
                .WithMessage("Project with Id {PropertyValue} does not exist.");

            // Validação para 'IdUser' (maior que zero e verificar existência no banco)
            RuleFor(x => x.IdUser)
                .GreaterThan(0).WithMessage("IdUser must be greater than 0.")
                .MustAsync(async (idUser, cancellation) => await _userRepository.Exists(idUser))
                .WithMessage("User with Id {PropertyValue} does not exist.");
        }
    }
}
