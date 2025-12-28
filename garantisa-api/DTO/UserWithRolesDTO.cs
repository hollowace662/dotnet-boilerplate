namespace garantisa.DTO
{
    public class UserWithRolesDTO
    {
        public required UserDTO User { get; set; }
        public required List<int> RoleIds { get; set; }
    }
}
