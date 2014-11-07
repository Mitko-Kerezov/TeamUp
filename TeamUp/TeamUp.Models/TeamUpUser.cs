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

        public TeamUpUser()
        {
            this.programmingCategories = new HashSet<ProgrammingCategory>();
            this.skills = new HashSet<Skill>();
        }

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

        public Occupation Occupation { get; set; }

    }

}
