using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {

        ICacheManager _cacheManager;
        public FunctionsController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        [HttpGet("getweatherinfo")]
        public async Task<IActionResult> GetWeatherInfo([FromQuery] string q)
        {
            var cacheResult = _cacheManager.Get($"weather_{q}");
            if(cacheResult != null)
            {
                return await Task.FromResult(Content(cacheResult.ToString(), MediaTypeHeaderValue.Parse("application/json")));
            }

            WebClient webClient = new WebClient();
            var json = await webClient.DownloadStringTaskAsync($"https://api.openweathermap.org/data/2.5/forecast?q={q}&appid=8b710326dd315dafbbb2761f4e9b0334");
            _cacheManager.Add($"weather_{q}", json, 60);
            return await Task.FromResult(Content(json,MediaTypeHeaderValue.Parse("application/json")));
        }
    }
}
