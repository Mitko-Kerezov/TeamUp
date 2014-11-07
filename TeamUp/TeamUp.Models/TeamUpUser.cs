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

        public TeamUpUser()
        {
            this.programmingCategories = new HashSet<ProgrammingCategory>();
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
        public Occupation Occupation { get; set; }

    }

}
