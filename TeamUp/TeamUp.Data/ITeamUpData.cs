namespace TeamUp.Data
{
    using TeamUp.Data.Repositories;
    using TeamUp.Models;

    public interface ITeamUpData
    {
        IGenericRepository<ChatMessage> ChatMessages { get; }

        IGenericRepository<ProgrammingCategory> Category { get; }

        IGenericRepository<Skill> Skills { get; }

        void SaveChanges();
    }
}
