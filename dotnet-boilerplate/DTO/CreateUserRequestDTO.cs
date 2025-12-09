using System.ComponentModel.DataAnnotations;

namespace dotnet_boilerplate.DTO
{
    public class CreateUserRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(30)]
        public string? Email { get; set; }

        [Required]
        public List<int>? RoleIds { get; set; }
    }
}
