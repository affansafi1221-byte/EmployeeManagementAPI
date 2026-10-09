using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.IService;
using MyFirstAPI.Dto;
using MyFirstAPI.IServices;
using MyFirstAPI.Services;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController( IAuthService authService)
            
        {
            _authService = AuthService;
        }
        [HttpPost("Login")]
    }   public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                var result = await _authService.LoginUser(userDto);

                if (result.Item1 == 0)
                {
                    return NotFound(result.Item2);
                }
                if (result.Item1 == 1)
                {
                    return BadRequest(result.Item2);
                }
                return Ok(result.Item2);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}