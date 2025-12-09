using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.DTO
{
    public class GetUserResponseDTO
    {
        public required User User { get; set; }
        public required List<int> Roles { get; set; }
    }
}
