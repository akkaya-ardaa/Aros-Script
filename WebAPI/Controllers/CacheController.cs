using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.CrossCuttingConcerns.Caching;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        ICategoryService _categoryService;
        ICacheManager _cacheManager;
        IHostApplicationLifetime _hostApplicationLifetime;
        public CacheController(ICategoryService categoryService,ICacheManager cacheManager,IHostApplicationLifetime hostApplicationLifetime)
        {
            _cacheManager = cacheManager;
            _categoryService = categoryService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        [HttpGet("clearall")]
        [Authorize]
        [SecuredOperation("admin")]
        public async Task<IActionResult> ClearAll([FromQuery] int confirm)
        {
            if(confirm != 1)
            {
                return await Task.FromResult(Ok(new { Message="Cache'leri silmek sistemin yavaşlamasına ve bir süreliğine durmasına sebep olacaktır! Eğer eminseniz lütfen isteğinize 'confirm=1' verisini ekleyiniz." }));
            }
            else
            {
                var categories = _categoryService.GetAll().Data;
                foreach(Category category in categories)
                {
                    _cacheManager.Remove($"articles_{category}");
                };
                return await Task.FromResult(Ok(new { Message="Cache'ler silindi." }));
            }
        }
    }
}
