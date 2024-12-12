namespace DVD_RENTAL_API.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User? Customer { get; set; } // Navigation property
        public int DVDId { get; set; }
        public DVD? DVD { get; set; } // Navigation property
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; } = false;
        public string status { get; set; } = "pending";

    }
}
