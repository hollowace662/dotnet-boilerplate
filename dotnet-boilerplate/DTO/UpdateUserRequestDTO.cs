using System.ComponentModel.DataAnnotations;

namespace dotnet_boilerplate.DTO
{
    public class UpdateUserRequestDTO
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string? Username { get; set; }

        [EmailAddress]
        [MinLength(5)]
        [MaxLength(30)]
        public string? Email { get; set; }

        public List<int>? RoleIds { get; set; }
    }
}
