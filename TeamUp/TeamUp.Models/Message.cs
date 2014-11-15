namespace TeamUp.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public Message()
        {
            this.DateSent = DateTime.Now;
        }

        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual TeamUpUser Author { get; set; }

        public string RecipientId { get; set; }

        public TeamUpUser Recipient { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }

        [DefaultValue(false)]
        public bool IsInvitation { get; set; }

    }
}
