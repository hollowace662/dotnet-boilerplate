using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_boilerplate.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(createUserDTO);
                return CreatedAtAction(
                    nameof(CreateUser),
                    new { id = createdUser.Id },
                    createdUser
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
