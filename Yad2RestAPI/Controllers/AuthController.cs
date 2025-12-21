using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yad2RestAPI.Models;
using Yad2RestAPI.Repositories;

namespace Yad2RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        public AuthController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            var result = await _profileRepository.RegisterAsync(registerRequestModel);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
        {
            var result = await _profileRepository.LoginAsync(loginRequestModel);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
