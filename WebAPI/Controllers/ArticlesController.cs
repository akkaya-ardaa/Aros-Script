using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Entities.Concrete;
using Core.Utilities.Results;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        IArticleService _articleService;
        ICacheManager _cacheManager;
        public ArticlesController(IArticleService articleService,ICacheManager cacheManager)
        {
            _articleService = articleService;
            _cacheManager = cacheManager;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            var result = _articleService.Search(q);
            return await Task.FromResult(Ok(result));
        }

        [HttpGet("getallbycategory")]
        public async Task<IActionResult> GetAllByCategory([FromQuery] int categoryId)
        {
            var cacheResult = _cacheManager.Get<IDataResult<List<Article>>>($"articles_{categoryId}");
            if(cacheResult != null)
            {
                Debug.WriteLine($"Load from cache! {categoryId}");
                return await Task.FromResult(Ok(cacheResult));
            }
            var dbResult = _articleService.GetByCategory(categoryId);
            _cacheManager.Add($"articles_{categoryId}", dbResult, 60);
            return await Task.FromResult(Ok(dbResult));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Article article)
        {
            article.AuthorId = int.Parse(HttpContext.User.Identity.GetUserId());
            article.AddedDate = DateTime.Now;
            article.LastEditDate = DateTime.Now;
            var result = _articleService.Add(article);
            if (result.Success)
            {
                _cacheManager.Remove($"articles_{article.CategoryId}");
                Debug.WriteLine($"cleaned cache articles_{article.CategoryId}");
                return await Task.FromResult(Ok(result));
            }
            return await Task.FromResult(BadRequest(result));
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery] int articleId)
        {
            var article = _articleService.GetById(articleId);
            var result = _articleService.Delete(articleId);
            if (result.Success)
            {
                _cacheManager.Remove($"articles_{article.CategoryId}"); //cache sil.
                Debug.WriteLine($"cleaned cache articles_{article.CategoryId}");
                return await Task.FromResult(Ok(result));
            }
            return await Task.FromResult(BadRequest(result));
        }

    }
}
