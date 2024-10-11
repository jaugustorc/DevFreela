using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            // Validação para 'IdProject' (maior que zero e verificar existência no banco)
            this.RuleFor(x => x.IdProject)
                .GreaterThan(1).WithMessage("IdProject must be greater than 0.");

            // Validação para 'Title' (não nulo, tamanho mínimo e máximo)
            this.RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters long.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            // Validação para 'Description' (não nulo, tamanho mínimo e máximo)
            this.RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            // Validação para 'TotalCost' (maior que zero)
            this.RuleFor(x => x.TotalCost)
                .GreaterThanOrEqualTo(1000).WithMessage("TotalCost must be greater than 1000.");
        }
    }
}
