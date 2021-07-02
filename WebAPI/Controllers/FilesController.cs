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
    public class FilesController : ControllerBase
    {
        [HttpGet("showarticleimage")]
        public async Task<IActionResult> ShowArticleImage([FromQuery] int id)
        {
            Byte[] imageBytes = await System.IO.File.ReadAllBytesAsync($"images/articles/{id}");
            return await Task.FromResult(File(imageBytes,"image/png"));
        }

        [HttpGet("showgalleryimage")]
        public async Task<IActionResult> ShowGalleryImage([FromQuery] int id)
        {
            Byte[] imageBytes = await System.IO.File.ReadAllBytesAsync($"images/gallery/{id}");
            return await Task.FromResult(File(imageBytes, "image/png"));
        }
    }
}
