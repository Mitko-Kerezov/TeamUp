namespace TeamUp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeamUp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamUp.Data.TeamUpDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TeamUp.Data.TeamUpDbContext";
        }

        protected override void Seed(TeamUp.Data.TeamUpDbContext context)
        {
            if (!context.ProgrammingCategories.Any())
            {
                var categories = new ProgrammingCategory[] { 
                new ProgrammingCategory(){Name = "Embedded"},
                new ProgrammingCategory(){Name = "Desktop"},
                new ProgrammingCategory(){Name = "Mobile"},
                new ProgrammingCategory(){Name = "Web"}
                };

                foreach (var category in categories)
                {
                    context.ProgrammingCategories.Add(category);
                }

                context.SaveChanges();
            }
        }
    }
}
