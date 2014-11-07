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
            }

            if (!context.Skills.Any())
            {
                var skills = new Skill[] { 
                new Skill(){Name = "C"},
                new Skill(){Name = "C#"},
                new Skill(){Name = "JavaScript"},
                new Skill(){Name = "Objective C"},
                new Skill(){Name = "Java"},
                new Skill(){Name = "Forth"},
                new Skill(){Name = "PHP"},
                new Skill(){Name = "Python"},
                new Skill(){Name = "Photoshop"},
                new Skill(){Name = "HTML"},
                new Skill(){Name = "CSS"},
                new Skill(){Name = "C++"}
                };

                foreach (var skill in skills)
                {
                    context.Skills.Add(skill);
                }
            }

            context.SaveChanges();
        }
    }
}
