using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueBlog.API.Helper;
using VueBlog.API.Models;
using VueBlog.API.Services.User;
using VueBlog.API.Utility;

namespace VueBlog.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController:ControllerBase
    {
        private readonly IUserService userService;
        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(User user)
        {
            var res = await userService.GetUserByNameAsync(user.Username);
            if(res == null)
            {
                return BadRequest("不存在此用户");
            }
            else
            {
                if(res.PasswordHash == PasswordHasher.HashPassword(user.PasswordHash,res.Salt))
                {
                    var token = JwtHelper.GenerateToken(user.Username, res.Role);
                    return Ok(new {token});
                }
                else
                {
                    return BadRequest("密码错误");
                }
            }

        }
        [HttpGet("secure")]
        [Authorize]
        public IActionResult SecureEndpoint()
        {
            return Ok("Login");
        }
    }
}
