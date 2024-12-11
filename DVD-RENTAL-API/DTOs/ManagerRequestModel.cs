namespace DVD_RENTAL_API.DTOs
{
    public class ManagerRequestModel
    {
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CopiesAvailable { get; set; }
        public decimal Price { get; set; } // Decimal for monetary value
        public IFormFile? ImageFile { get; set; }
    }
}
