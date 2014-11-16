namespace TeamUp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TeamUp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamUp.Data.TeamUpDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TeamUp.Data.TeamUpDbContext";
        }

        protected override void Seed(TeamUpDbContext context)
        {
            var userStore = new UserStore<TeamUpUser>(context);
            var userManager = new UserManager<TeamUpUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            this.SeedCategories(context);
            this.SeedSkills(context);
            this.SeedRoles(context, roleManager);
            this.SeedUsersAndAdmins(context, userManager);
            this.SeedProjects(context);
        }

        private void SeedProjects(TeamUpDbContext context)
        {
            if (!context.Projects.Any())
            {
                var admin = context.Users.First(u => u.Email == "admin@gmail.com");
                var project = new Project()
                {
                    Name = "Project X",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                };

                admin.Projects.Add(project);
                project.Users.Add(admin);
                context.Projects.Add(project);
                context.SaveChanges();
            }
        }

        private void SeedUsersAndAdmins(TeamUpDbContext context, UserManager<TeamUpUser> manager)
        {
            if (!context.Users.Any())
            {
                var admin = new TeamUpUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Occupation = Occupation.Developer,
                };

                manager.Create(admin, "123456");

                manager.AddToRole(admin.Id, "Admin");

                manager.Create(new TeamUpUser()
                {
                    UserName = "devOne@gmail.com",
                    Email = "devOne@gmail.com",
                    Occupation = Occupation.Developer,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "devTwo@gmail.com",
                    Email = "devTwo@gmail.com",
                    Occupation = Occupation.Developer,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "devThree@gmail.com",
                    Email = "devThree@gmail.com",
                    Occupation = Occupation.Developer,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "frontOne@gmail.com",
                    Email = "frontOne@gmail.com",
                    Occupation = Occupation.FrontEnd,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "frontTwo@gmail.com",
                    Email = "frontTwo@gmail.com",
                    Occupation = Occupation.FrontEnd,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "frontThree@gmail.com",
                    Email = "frontThree@gmail.com",
                    Occupation = Occupation.FrontEnd,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "QAOne@gmail.com",
                    Email = "QAOne@gmail.com",
                    Occupation = Occupation.QA,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "QATwo@gmail.com",
                    Email = "QATwo@gmail.com",
                    Occupation = Occupation.QA,
                }, "123456");

                manager.Create(new TeamUpUser()
                {
                    UserName = "QAThree@gmail.com",
                    Email = "QAThree@gmail.com",
                    Occupation = Occupation.QA,
                }, "123456");

                var random = new Random();

                this.LinkUsersToCategories(context, random);
                this.LinkUsersToSkills(context, random);
            }
        }

        private void LinkUsersToSkills(TeamUpDbContext context, Random random)
        {
            var allUsers = context.Users.ToList();
            var allSkills = context.Skills.ToList();

            foreach (var user in allUsers)
            {
                foreach (var skill in allSkills)
                {
                    if (random.Next() % 2 == 0)
                    {
                        user.Skills.Add(skill);
                        skill.Users.Add(user);
                    }
                }
            }
        }

        private void LinkUsersToCategories(TeamUpDbContext context, Random random)
        {
            var allUsers = context.Users.ToList();
            var allCategories = context.ProgrammingCategories.ToList();

            foreach (var user in allUsers)
            {
                foreach (var category in allCategories)
                {
                    if (random.Next() % 2 == 0)
                    {
                        user.ProgrammingCategories.Add(category);
                        category.Users.Add(user);
                    }
                }
            }
        }

        private void SeedRoles(TeamUpDbContext context, RoleManager<IdentityRole> manager)
        {
            if (!context.Roles.Any())
            {
                manager.Create(new IdentityRole("Admin"));
            }

            context.SaveChanges();
        }

        private void SeedSkills(TeamUpDbContext context)
        {
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

        private void SeedCategories(TeamUpDbContext context)
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

            context.SaveChanges();
        }
    }
}
