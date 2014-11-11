namespace TeamUp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        private ICollection<TeamUpUser> users;

        public Project()
        {
            this.users = new HashSet<TeamUpUser>();
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<TeamUpUser> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }

        public bool HasEnded { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
