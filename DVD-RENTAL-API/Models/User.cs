namespace DVD_RENTAL_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Store hashed passwords
        public string NIC { get; set; }
        public string? Phone { get; set; } // Optional field
        public string Role { get; set; } // For example, "admin" or "user"
    }
}
