namespace TeamUp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProgrammingCategory
    {
        private ICollection<TeamUpUser> users;

        public ProgrammingCategory()
        {
            this.users = new HashSet<TeamUpUser>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [MaxLength(50)]
        public string Name { get; set; }


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
    }
}
