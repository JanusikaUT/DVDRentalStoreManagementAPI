namespace DVD_RENTAL_API.Models
{
    public class DVD
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }

        // New Price Property
        public decimal Price { get; set; } // Using decimal for monetary values
        public DateTime ReleaseDate { get; set; }
        public int CopiesAvailable { get; set; }
        public string? ImagePath { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
