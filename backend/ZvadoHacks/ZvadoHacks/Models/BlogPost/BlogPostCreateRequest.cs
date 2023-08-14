using Microsoft.AspNetCore.Mvc;

namespace ZvadoHacks.Models.BlogPost
{
    public class BlogPostCreateRequest
    {
        public string Title { get; set; }


        public string Content { get; set; }
    }
}
