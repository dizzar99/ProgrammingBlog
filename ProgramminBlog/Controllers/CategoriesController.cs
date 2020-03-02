using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleCategoryManagment;

namespace ProgramminBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IArticleCategoryService categoryService;

        public CategoriesController(IArticleCategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await this.categoryService.GetCategories();
            return this.Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(string categoryId)
        {
            var category = await this.categoryService.GetCategory(categoryId);
            return this.Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategory)
        {
            var created = await this.categoryService.CreateCategory(createCategory);
            var location = created.Id;
            return this.Created(location, created);
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(string categoryId, CreateCategoryRequest updateRequest)
        {
            var updated = await this.categoryService.UpdateCategory(categoryId, updateRequest);
            return this.Ok(updated);
        }

        [HttpDelete("{categotyId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            await this.categoryService.DeleteCategory(categoryId);
            return this.NoContent();
        }
    }
}