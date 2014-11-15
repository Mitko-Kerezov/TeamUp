namespace TeamUp.Web.Models.Messages
{
    using System;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class ReadMessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public TeamUpUser Author { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsRead { get; set; }
    }
}