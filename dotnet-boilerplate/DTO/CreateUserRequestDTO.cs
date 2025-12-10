namespace dotnet_boilerplate.DTO
{
    public class CreateUserRequestDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
