using Business.Abstract;
using Entities.Concrete;
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
    public class CommentsController : ControllerBase
    {
        ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("getcommentsbyarticle")]
        public async Task<IActionResult> GetCommentsByArticle([FromQuery] int articleId)
        {
            var result = _commentService.GetCommentsByArticle(articleId);
            return await Task.FromResult(Ok(result));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Comment comment)
        {
            var result = _commentService.Add(comment);
            if (result.Success)
            {
                return await Task.FromResult(Ok(result));
            }
            return await Task.FromResult(BadRequest(result));
        }
    }
}
