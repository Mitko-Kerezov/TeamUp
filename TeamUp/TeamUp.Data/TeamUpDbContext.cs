using Microsoft.AspNet.Identity.EntityFramework;
using TeamUp.Data.Migrations;
using TeamUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.Data
{
    public class TeamUpDbContext : IdentityDbContext<TeamUpUser>, ITeamUpContext
    {
        public TeamUpDbContext()
            : base("TeamUpConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TeamUpDbContext, Configuration>());
        }

        public IDbSet<ChatMessage> ChatMessages { get; set; }

        public IDbSet<ProgrammingCategory> Categories { get; set; }

        public IDbSet<Skill> Skills { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static TeamUpDbContext Create()
        {
            return new TeamUpDbContext();
        }
    }
}
