using DevFreela.Application.Commands.InsertSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertSkillValidator : AbstractValidator<InsertSkillCommand>
    {
        public InsertSkillValidator()
        {
            // Validação para 'Description' (não nulo, tamanho mínimo e máximo)
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        }
    }
}
