using Microsoft.AspNetCore.Mvc;

namespace ZvadoHacks.Models.BlogPost
{
    public class BlogPostListReqest
    {
        [FromQuery]
        public string? FilterField { get; set; }

        [FromQuery]
        public string? FilterValue { get; set; }

        [FromQuery]
        public string? SortField { get; set; }

        [FromQuery]
        public int PageSize { get; set; }

        [FromQuery]
        public int PageNumber { get; set; }
    }
}
