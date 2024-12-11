namespace DVD_RENTAL_API.DTOs
{
    public class ManagerResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CopiesAvailable { get; set; }
        public string ImageUrl { get; set; }

        // Add Price
        public decimal Price { get; set; } // Decimal for monetary value
    }
}
