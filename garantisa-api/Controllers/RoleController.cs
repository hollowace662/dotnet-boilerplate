using garantisa.DTO;
using garantisa.Services;
using Microsoft.AspNetCore.Mvc;

namespace garantisa.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDTO createRoleDTO)
        {
            var createdRole = await _roleService.CreateRoleAsync(createRoleDTO);
            return CreatedAtAction(
                nameof(CreateRole),
                new { id = createdRole.RoleId },
                createdRole
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(
            [FromRoute] int id,
            [FromBody] UpdateRoleRequestDTO updateRoleDTO
        )
        {
            var updatedRole = await _roleService.UpdateRoleAsync(id, updateRoleDTO);
            if (updatedRole == null)
            {
                return NotFound();
            }
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var deleted = await _roleService.DeleteRoleAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
