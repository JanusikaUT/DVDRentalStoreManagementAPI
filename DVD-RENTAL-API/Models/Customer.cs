using System.ComponentModel.DataAnnotations;

namespace DVD_RENTAL_API.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string NIC { get; set; } // Unique Identifier for Customer

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }
    }
}

