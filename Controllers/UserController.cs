using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VueBlog.API.Models;
using VueBlog.API.Services.User;
using VueBlog.API.Utility;

namespace VueBlog.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("id")]
        public async Task<ActionResult<UserDTO>> GetUser([FromQuery]Guid id)
        {
            try
            {
                var resp = await userService.SearchByIdAsync(id);
                if (resp == null)
                {
                    return NotFound("没有找到此用户");

                }
                return Ok(UserDtoConverter.Converter(resp));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }

        [HttpPost("register/admin")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> RegisterAdmin([FromBody] User user)
        {
            try
            {
                if(!userService.IsNameConflict(user.Username))
                {
                    var result = await userService.CreateUserAsync(user,"Admin");
                    return Ok(UserDtoConverter.Converter(result)); // 返回 UserDTO
                }else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "用户名重复");
                }
            }

            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }
        [HttpPost("register/user")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] User user)
        {
            try
            {
                if (!userService.IsNameConflict(user.Username))
                {
                    var result = await userService.CreateUserAsync(user,"User");
                    return Ok(UserDtoConverter.Converter(result)); // 返回 UserDTO
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "用户名重复");
                }
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> DeleteUser(Guid id)
        {
            try
            {
                User res = await userService.SoftDeleteAsync(id);
                return Ok(UserDtoConverter.Converter(res));

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }

        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> UpdateUser(Guid id,User user)
        {
            try
            {
                User res = await userService.UpdateUserAsync(id,user);
                return Ok(UserDtoConverter.Converter(res));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }
        [HttpGet("name")]

        public async Task<ActionResult<UserDTO>> GetUserByNameAsync([FromQuery]string name)
        {
            try
            {
                User res = await userService.GetUserByNameAsync(name);
                if (res != null)
                {
                    return Ok(UserDtoConverter.Converter(res));
                }
                return NotFound("没有找到此用户");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUserAsnyc()
        {
            try
            {
                List<User> users = await userService.GetAllUsersAsync();
                return Ok(UserDtoConverter.Converter(users));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "服务器内部错误");
            }
        }
    }
}
