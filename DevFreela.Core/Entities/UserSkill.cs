namespace DevFreela.Core.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int idUser, int idSkill) : base()
        {
            IdUser = idUser;
            IdSkill = idSkill;
        }

        public int IdUser { get; private set; }
        public User User { get; private set; }
        public int IdSkill { get; private set; }
        public Skill Skill { get; private set; }
    }
}
