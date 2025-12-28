namespace garantisa.DTO
{
    public class UpdateUserRequestDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
