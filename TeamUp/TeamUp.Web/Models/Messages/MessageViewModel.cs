namespace TeamUp.Web.Models.Messages
{
    using System.ComponentModel.DataAnnotations;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        [Required]
        public string RecipientId { get; set; }

        public TeamUpUser Recipient { get; set; }

        [Required]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

    }
}