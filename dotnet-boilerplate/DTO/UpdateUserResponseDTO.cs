using dotnet_boilerplate.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace dotnet_boilerplate.DTO
{
    public class UpdateUserResponseDTO
    {
        public required User User { get; set; }
        public required List<int> RoleIds { get; set; }
    }
}
