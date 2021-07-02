using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("listarticleimages")]
        public async Task<IActionResult> ListArticleImages()
        {
            var result = Directory.GetFiles("images/article");
            var temp = new List<string>() { };
            foreach (var item in result)
            {
                temp.Add(item.Split("\\")[1]);
            }
            return await Task.FromResult(Ok(temp));
        }

        [HttpGet("listgalleryimages")]
        public async Task<IActionResult> ListGalleryImages()
        {
            var result = Directory.GetFiles("images/gallery");
            var temp = new List<string>() { };
            foreach (var item in result)
            {
                temp.Add(item.Split("\\")[1]);
            }
            return await Task.FromResult(Ok(temp));
        }

        [HttpGet("showarticleimage")]
        public async Task<IActionResult> ShowArticleImage([FromQuery] int id)
        {
            Byte[] imageBytes = await System.IO.File.ReadAllBytesAsync($"images/article/{id}");
            return await Task.FromResult(File(imageBytes,"image/png"));
        }

        [HttpGet("showgalleryimage")]
        public async Task<IActionResult> ShowGalleryImage([FromQuery] int id)
        {
            Byte[] imageBytes = await System.IO.File.ReadAllBytesAsync($"images/gallery/{id}");
            return await Task.FromResult(File(imageBytes, "image/png"));
        }

        [HttpPost("uploadarticleimage")]
        public async Task<IActionResult> UploadArticleImage([FromForm] IFormFile formFile)
        {
            var id = new Random().Next(111111111, 999999999);
            Stream stream = new FileStream($"images/article/{id}", FileMode.CreateNew);
            await formFile.CopyToAsync(stream);
            stream.Close();
            return await Task.FromResult(Ok(id));
        }

        [HttpPost("uploadgalleryimage")]
        public async Task<IActionResult> UploadGalleryImage([FromForm] IFormFile formFile)
        {
            var id = new Random().Next(111111111, 999999999);
            Stream stream = new FileStream($"images/gallery/{id}", FileMode.CreateNew);
            await formFile.CopyToAsync(stream);
            stream.Close();
            return await Task.FromResult(Ok(id));
        }
    }
}
