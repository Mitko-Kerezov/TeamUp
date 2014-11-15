﻿using System;
namespace TeamUp.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual TeamUpUser Author { get; set; }

        public string RecipientId { get; set; }

        public virtual TeamUpUser Recipient { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsRead { get; set; }

    }
}
