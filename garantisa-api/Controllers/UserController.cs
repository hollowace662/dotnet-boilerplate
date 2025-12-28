using garantisa.DTO;
using garantisa.Services;
using Microsoft.AspNetCore.Mvc;

namespace garantisa.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO createUserDTO)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(createUserDTO);
                return CreatedAtAction(
                    nameof(CreateUser),
                    new { id = createdUser.User.UserId },
                    createdUser
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] int id,
            [FromBody] UpdateUserRequestDTO updateUserDTO
        )
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, updateUserDTO);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound(new { message = "User not found." });
            }
            return Ok(new { message = $"User {id} deleted successfully." });
        }
    }
}
