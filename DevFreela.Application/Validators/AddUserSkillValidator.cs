using DevFreela.Application.Commands.AddUserSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class AddUserSkillValidator: AbstractValidator<AddUserSkillCommand>
    {

        public AddUserSkillValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID must be greater than 0.");
                
            // Validação para garantir que o array não seja nulo ou vazio
            RuleFor(x => x.SkillIds)
                .NotNull().WithMessage("SkillIds cannot be null.")
                .NotEmpty().WithMessage("SkillIds cannot be empty.");

            // Validação para garantir que cada SkillId seja maior que 0
            RuleForEach(x => x.SkillIds)
                .GreaterThan(0).WithMessage("Each SkillId must be greater than 0.");

        }
    }
}
