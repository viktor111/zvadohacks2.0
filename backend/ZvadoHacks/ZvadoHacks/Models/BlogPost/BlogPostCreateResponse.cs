﻿namespace ZvadoHacks.Models.BlogPost
{
    public class BlogPostCreateResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
