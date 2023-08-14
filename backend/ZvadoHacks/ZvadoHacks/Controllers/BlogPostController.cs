using Euroins.Payment.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Data.Repositories;
using ZvadoHacks.Infrastructure.Extensions;
using ZvadoHacks.Models.BlogPost;
using ZvadoHacks.Models.Shared;

namespace ZvadoHacks.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IRepository<BlogPost> _blogPostRepository;

        public BlogPostController(IRepository<BlogPost> blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPostCreateRequest blogPostCreateRequest)
        {
            var blogPost = await _blogPostRepository.Add(new BlogPost
            {
                Content = blogPostCreateRequest.Content,
                Title = blogPostCreateRequest.Title,
            });

            var response = new BaseResponse<BlogPost>();
            response.SetData(blogPost);

            return Ok(response.Success());
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] BlogPostListReqest blogPostListReqest)
        {
            Expression<Func<BlogPost, bool>>? filterExpression = null;

            if (!string.IsNullOrEmpty(blogPostListReqest.FilterField))
            {
                var param = Expression.Parameter(typeof(BlogPost), "x");
                var prop = Expression.Property(param, blogPostListReqest.FilterField);
                var value = Expression.Constant(blogPostListReqest.FilterValue);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(prop, containsMethod, value);
                filterExpression = Expression.Lambda<Func<BlogPost, bool>>(containsExpression, param);
            }

            Expression<Func<BlogPost, object>>? sortExpression = null;

            if (!string.IsNullOrEmpty(blogPostListReqest.SortField))
            {
                var param = Expression.Parameter(typeof(BlogPost), "x");
                var prop = Expression.Property(param, blogPostListReqest.SortField);
                var conversion = Expression.Convert(prop, typeof(object));
                sortExpression = Expression.Lambda<Func<BlogPost, object>>(conversion, param);
            }

            var result = await _blogPostRepository.GetPaginated(blogPostListReqest.PageNumber, blogPostListReqest.PageSize, filterExpression, sortExpression);

            var response = new BaseResponse<GetPaginatedDto<BlogPost>>();
            response.SetData(result);

            return Ok(response.Success());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = new BaseResponse<BlogPost>();

            var post = await _blogPostRepository.FindOne(x => x.Id == id);

            if (post is null)
            {                
                return NotFound(response.Error("Blog post not found!"));
            }

            var deleted = await _blogPostRepository.Delete(post);

            response.SetData(deleted);

            return Ok(response.Success());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, BlogPostUpdateRequest blogPostUpdateRequest)
        {
            var response = new BaseResponse<BlogPost>();

            var post = await _blogPostRepository.FindOne(x => x.Id == id);

            if (post is null)
            {
                return NotFound(response.Error("Blog post not found"));
            }

            if(string.IsNullOrEmpty(blogPostUpdateRequest.Title) || string.IsNullOrEmpty(blogPostUpdateRequest.Content))
            {
                return BadRequest(response.Error("Invalid input data"));
            }

            post.Title = blogPostUpdateRequest.Title;
            post.Content = blogPostUpdateRequest.Content;

            await _blogPostRepository.Update(post);

            return Ok(post);
        }
    }
}
