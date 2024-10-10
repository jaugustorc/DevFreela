using DevFreela.Application.Commands.AddUserSkill;
using DevFreela.Core.Repositories;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class AddUserSkillValidator: AbstractValidator<AddUserSkillCommand>
    {
        private readonly ISkillsRepository _skillRepository;
        private readonly IUsersRepository _usersRepository;

        public AddUserSkillValidator(ISkillsRepository skillRepository, IUsersRepository usersRepository)
        {
            _skillRepository = skillRepository;
            _usersRepository = usersRepository;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID must be greater than 0.");

            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellation) => await _usersRepository.Exists(id))
                .WithMessage("ID not found in database.");

            // Validação para garantir que o array não seja nulo ou vazio
            RuleFor(x => x.SkillIds)
                .NotNull().WithMessage("SkillIds cannot be null.")
                .NotEmpty().WithMessage("SkillIds cannot be empty.");

            // Validação para garantir que cada SkillId seja maior que 0
            RuleForEach(x => x.SkillIds)
                .GreaterThan(0).WithMessage("Each SkillId must be greater than 0.");

            // Validação personalizada para verificar se cada SkillId existe no banco
            RuleForEach(x => x.SkillIds)
                .MustAsync(async (skillId, cancellation) => await _skillRepository.Exists(skillId))
                .WithMessage("SkillId {PropertyValue} does not exist.");

        }
    }
}
