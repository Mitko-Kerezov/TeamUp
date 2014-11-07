namespace TeamUp.Data
{
    using TeamUp.Data.Repositories;
    using TeamUp.Models;

    public interface ITeamUpData
    {
        IGenericRepository<ChatMessage> ChatMessages { get; }

        IGenericRepository<Skill> Skills { get; }

        IGenericRepository<ProgrammingCategory> ProgrammingCategories { get; }

        IGenericRepository<TeamUpUser> Users { get; }

        void SaveChanges();
    }
}
