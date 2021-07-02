using Business.Abstract;
using Entities.Dto;
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
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthorForLoginDto authorForLoginDto)
        {
            var result = _authService.Login(authorForLoginDto);
            if (!result.Success)
            {
                return await Task.FromResult(BadRequest(result));
            }
            var token = _authService.CreateAccessToken(result.Data);
            if (!token.Success)
            {
                return await Task.FromResult(BadRequest(token));
            }
            return await Task.FromResult(Ok(token));
        }
    }
}
