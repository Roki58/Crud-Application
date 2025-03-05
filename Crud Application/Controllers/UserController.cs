using Crud_Application.Models;
using Crud_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;

           
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            var response =await _userService.CreateUserAsync(user);
            return Ok(response);
            
        }
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _userService.GetAllUsersAsync();
            return Ok(response);

        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return Ok(response);

        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdatedUserAsync(User user)
        {
            var response = await _userService.UpdatedUserAsync(user);
            return Ok(response);

        }
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUserByIdAsync(int id)
        {
            var response = await _userService.DeleteUserByIdAsync(id);
            return Ok(response);

        }

    }
}
