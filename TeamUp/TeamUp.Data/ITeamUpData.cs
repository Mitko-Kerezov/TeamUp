namespace TeamUp.Data
{
    using TeamUp.Data.Repositories;
    using TeamUp.Models;

    public interface ITeamUpData
    {
        ITeamUpContext Context { get; }

        IGenericRepository<Message> Messages { get; }

        IGenericRepository<Skill> Skills { get; }

        IGenericRepository<ProgrammingCategory> ProgrammingCategories { get; }

        IGenericRepository<Project> Projects { get; }

        IGenericRepository<TeamUpUser> Users { get; }

        void SaveChanges();
    }
}
