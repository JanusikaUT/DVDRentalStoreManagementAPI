namespace DVD_RENTAL_API.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NIC { get; set; }
        public string? Phone { get; set; } // Optional field
        public string Role { get; set; }
    }
}
