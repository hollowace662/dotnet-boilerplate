using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_boilerplate.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO)
        {
            var createdRole = await _roleService.CreateRoleAsync(createRoleDTO);
            return CreatedAtAction(nameof(CreateRole), new { id = createdRole.Id }, createdRole);
        }

        //Read
        [HttpGet("{id}")]
        public IActionResult GetRole(GetRoleDTO getRoleDTO)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(new { response = "In GetAllRoles" });
        }

        //Update
        [HttpPut("{id}")]
        public IActionResult UpdateRole(UpdateRoleDTO updateRoleDTO)
        {
            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(DeleteRoleDTO deleteRoleDTO)
        {
            return Ok();
        }
    }
}
