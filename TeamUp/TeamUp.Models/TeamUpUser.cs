namespace TeamUp.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class TeamUpUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TeamUpUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

        }

        // Additional fields
        private ICollection<ProgrammingCategory> programmingCategories;
        private ICollection<Skill> skills;
        private ICollection<Project> projects;
        private ICollection<Message> myMessages;

        public TeamUpUser()
        {
            this.programmingCategories = new HashSet<ProgrammingCategory>();
            this.skills = new HashSet<Skill>();
            this.projects = new HashSet<Project>();
            this.myMessages = new HashSet<Message>();
        }

        public Occupation Occupation { get; set; }

        public ThemeChoice ThemeChosen { get; set; }

        public virtual ICollection<ProgrammingCategory> ProgrammingCategories
        {
            get
            {
                return this.programmingCategories;
            }
            set
            {
                this.programmingCategories = value;
            }
        }

        public virtual ICollection<Skill> Skills
        {
            get
            {
                return this.skills;
            }
            set
            {
                this.skills = value;
            }
        }

        public virtual ICollection<Project> Projects
        {
            get
            {
                return this.projects;
            }
            set
            {
                this.projects = value;
            }
        }

        public virtual ICollection<Message> MyMessages
        {
            get
            {
                return this.myMessages;
            }
            set
            {
                this.myMessages = value;
            }
        }
    }
}
