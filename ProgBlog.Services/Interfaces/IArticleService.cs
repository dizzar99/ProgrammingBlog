using ProgBlog.Services.Models;
using ProgBlog.Services.Models.ArticleManagment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDetails> GetArticle(string articleId);
        Task<IEnumerable<ArticleListItem>> GetArticles(int count);
        Task<IEnumerable<ArticleListItem>> GetArticles(IList<string> ids);
        Task<ArticleDetails> CreateArticle(CreateArticleRequest article);
        Task<ArticleDetails> UpdateArticle(string articleId, UpdateArticleRequest article);
        Task Remove(string articleId);
    }
}
