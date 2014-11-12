namespace TeamUp.Web.Models
{
    using System.Collections.Generic;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;
    
    public class UserDetailsViewModel : IMapFrom<TeamUpUser>
    {
        public string Email { get; set; }

        public ICollection<ProgrammingCategory> ProgrammingCategories { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<Project> Projects { get; set; }
    }


}