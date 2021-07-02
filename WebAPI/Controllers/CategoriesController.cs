using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = _categoryService.GetAll();
            return await Task.FromResult(Ok(result));
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add(Category category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
            {
                return await Task.FromResult(Ok(result));
            }
            return await Task.FromResult(BadRequest(result));
        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Category category)
        {
            var result = _categoryService.Delete(category);
            if (result.Success)
            {
                return await Task.FromResult(Ok(result));
            }
            return await Task.FromResult(BadRequest(result));
        }
    }
}
