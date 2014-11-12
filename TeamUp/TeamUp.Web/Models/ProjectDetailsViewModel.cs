namespace TeamUp.Web.Models
{
    using System;
    using System.Collections.Generic;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class ProjectDetailsViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TeamUpUser> Users { get; set; }

        public bool HasEnded { get; set; }

        public DateTime DateCreated { get; set; }
    }
}