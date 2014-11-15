﻿namespace TeamUp.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using TeamUp.Models;

    public interface ITeamUpContext
    {
        IDbSet<Message> Messages { get; set; }

        IDbSet<Skill> Skills { get; set; }

        IDbSet<ProgrammingCategory> ProgrammingCategories { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
