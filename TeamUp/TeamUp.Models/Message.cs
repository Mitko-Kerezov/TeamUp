namespace TeamUp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public Message()
        {
            this.DateSent = DateTime.Now;
            this.IsRead = false;
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

        public bool IsRead { get; set; }

    }
}
