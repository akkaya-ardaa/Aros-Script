using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public async Task<IActionResult> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Debug.WriteLine(context.Error.Message);
            return await Task.FromResult(BadRequest(new { Message="Sistem işleminizi gerçekleştiremedi. Bunun sunucudan kaynaklandığını düşünüyorsanız lütfen destek hattından bizimle iletişime geçiniz." }));
        }
    }
}
