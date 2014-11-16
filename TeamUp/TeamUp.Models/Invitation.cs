namespace TeamUp.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Invitation
    {
        public Invitation()
        {
            this.DateSent = DateTime.Now;
        }

        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual TeamUpUser Author { get; set; }

        public string RecipientId { get; set; }

        public TeamUpUser Recipient { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [DefaultValue(InvitationResponseType.NotRead)]
        public InvitationResponseType InvitationResponse { get; set; }
    }
}
