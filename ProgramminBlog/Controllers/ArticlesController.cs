using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;

namespace ProgramminBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public ArticlesController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<IEnumerable<ArticleListItem>> Get()
        {
            return await this.articleService.GetArticles(10);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ArticleDetails> Get(string id)
        {
            return await this.articleService.GetArticle(id);
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<ArticleDetails> Post([FromBody] CreateArticleRequest createArticle)
        {
            var article = await this.articleService.CreateArticle(createArticle);
            await this.userService.AddArticlesToUser(createArticle.AuthorId, new string[] { article.Id });

            return article;
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<ArticleDetails> Put(string id, [FromBody] UpdateArticleRequest updateArticle)
        {
            var article = await this.articleService.UpdateArticle(id, updateArticle);
            return article;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string articleId)
        {
            var article = await this.articleService.GetArticle(articleId);
            await this.articleService.Remove(articleId);
            await this.userService.RemoveArticlesFromUser(article.AuthorId, new string[] { articleId });
        }
    }
}
